using System.Globalization;
using eSlozka.Application.Validations.Users;
using eSlozka.Application.ViewModels;
using eSlozka.Core.Commands.Users;
using eSlozka.Core.Security;
using eSlozka.Data;
using eSlozka.Domain.Constants;
using eSlozka.Domain.Enums;
using eSlozka.Domain.Models;
using eSlozka.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eSlozka.Web;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddSqlite(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddOptions<DataContextOptions>()
            .Bind(configuration.GetSection(DataContextOptions.SectionName))
            .Validate(options =>
            {
                var valid = Directory.Exists(options.Location)
                            && Path.GetFileNameWithoutExtension(options.FileNameWithExtension) is { Length: > 0 }
                            && Path.HasExtension(options.FileNameWithExtension);

                return valid;
            })
            .ValidateOnStart();

        var contextOptions = configuration.GetSection(DataContextOptions.SectionName).Get<DataContextOptions>();

        ArgumentNullException.ThrowIfNull(contextOptions, nameof(contextOptions));

        services.AddTransient<ISaveChangesInterceptor, AuditingSaveChangeInterceptor>();

        services.AddDbContextFactory<DataContext>(options =>
        {
            var dataSource = Path.Combine(contextOptions.Location, contextOptions.FileNameWithExtension);

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                DataSource = dataSource
            };

            var connectionString = connectionStringBuilder.ConnectionString;

            options.UseSqlite(connectionString);
            options.EnableDetailedErrors(environment.IsDevelopment());
            options.EnableSensitiveDataLogging(environment.IsDevelopment());
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        });

        return services;
    }

    public static WebApplication UseSqliteMigration(this WebApplication application)
    {
        var services = application.Services;
        var environment = application.Environment;

        using var scope = services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        if (environment.IsDevelopment())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        else
        {
            context.Database.EnsureCreated();
        }

        return application;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<ViewModelBase>()
            .AddClasses(classes => classes.AssignableTo<ViewModelBase>())
            .AsSelf()
            .WithScopedLifetime());

        return services;
    }

    public static WebApplication UseLocalizationResources(this WebApplication application)
    {
        var supportedCultures = new[] { new CultureInfo(Locales.English), new CultureInfo(Locales.Czech) };

        application.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(Locales.Default),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        return application;
    }

    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(RegisterCommandValidator).Assembly);

        return services;
    }

    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly);
        });

        return services;
    }

    public static IServiceCollection AddUtilities(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<HashProviderOptions>();
        services.AddSingleton<HashProvider>();
        services.AddSingleton<IHashProvider, HashProvider>();

        return services;
    }

    public static WebApplication UseSqliteSeeder(this WebApplication application)
    {
        var services = application.Services;

        using var scope = services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IHashProvider>();

        var administrator = new User
        {
            GivenName = "System",
            FamilyName = "Administrator",
            Email = "system.administrator@provider.dev",
            EmailConfirmed = true,
            Locale = Locales.Default
        };

        (administrator.Password, administrator.PasswordSalt) = hasher.GetHash("Password123$");

        context.Users.Add(administrator);

        var manager = new User
        {
            GivenName = "System",
            FamilyName = "Manager",
            Email = "system.manager@provider.dev",
            EmailConfirmed = true,
            Locale = Locales.Default
        };

        (manager.Password, manager.PasswordSalt) = hasher.GetHash("Password123$");

        context.Users.Add(manager);

        context.SaveChanges();

        var roles = context.Roles.AsNoTracking().ToList();

        foreach (var role in roles)
        {
            var roleAssignment = new UserRole
            {
                UserID = administrator.UserID,
                RoleID = role.RoleID
            };

            context.UserRoles.Add(roleAssignment);
        }

        foreach (var role in roles.Where(role => role.Permission != Permission.All))
        {
            var roleAssignment = new UserRole
            {
                UserID = manager.UserID,
                RoleID = role.RoleID
            };

            context.UserRoles.Add(roleAssignment);
        }

        context.SaveChanges();

        return application;
    }
}
