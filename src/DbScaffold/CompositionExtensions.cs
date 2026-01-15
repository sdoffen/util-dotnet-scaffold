using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace DbScaffold;

public static class CompositionExtensions
{
    public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Call extension methods to register each interface implementation.

        var databaseOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>()
            ?? throw new InvalidOperationException($"Missing {DatabaseOptions.SectionName} section in configuration.");

        var optionsAction = GetDbContextOptionsAction(databaseOptions);

        services.AddDbContextFactory<SampleDbContext>(optionsAction);

        if (databaseOptions.UseDbContextPooling)
        {
            services.AddDbContextPool<SampleDbContext>(optionsAction);
        }
        else
        {
            services.AddDbContext<SampleDbContext>(optionsAction);
        }

        services.AddHealthChecks()
            .AddDbContextCheck<SampleDbContext>("Project Database");

        return services;
    }

    private static Action<DbContextOptionsBuilder> GetDbContextOptionsAction(DatabaseOptions options)
    {
        return builder =>
        {
            if (options.AsNoTracking)
            {
                builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }

            builder.UseNpgsql(GetConnectionString(options), contextOptions =>
            {
                if (options.UseMigrationsAssembly && !string.IsNullOrEmpty(options.MigrationsAssembly))
                {
                    contextOptions.MigrationsAssembly(options.MigrationsAssembly);
                }

                if (options.EnableRetryOnFailure)
                {
                    contextOptions.EnableRetryOnFailure(
                        options.MaxRetryCount,
                        TimeSpan.FromSeconds(options.MaxRetryDelaySeconds),
                        options.ErrorCodesToAdd
                    );
                }
            });
        };
    }

    private static string GetConnectionString(DatabaseOptions options)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = options.Host,
            Port = options.Port,
            Database = options.DatabaseName,
            Username = options.Username,
        };

        if (!string.IsNullOrEmpty(options.Password))
        {
            builder.Password = options.Password;
        }
        else
        {
            // Handle the case where the password is not provided.
            throw new InvalidOperationException("Password is required for the connection string.");
        }

        return builder.ConnectionString;
    }
}
