using System.Globalization;
using eSlozka.Application.Validations.Users;
using eSlozka.Application.ViewModels;
using eSlozka.Core.Commands.Users;
using eSlozka.Core.Utilities;
using eSlozka.Data;
using eSlozka.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eSlozka.Web;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddServerlessDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
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

    public static WebApplication UseServerlessDatabaseAutoMigration(this WebApplication application)
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
        var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("cs") };

        application.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en"),
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
}
