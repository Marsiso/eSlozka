using eSlozka.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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
}