using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbScaffold.Tests.Attributes;

internal static class DatabaseAvailabilityChecker
{
    public static readonly string DatabaseUnavailableMessage = "🚫 SKIPPED: Database not available";

    private static bool? _isDatabaseAvailable;

    public static bool IsDatabaseAvailable
    {
        get
        {
            if (_isDatabaseAvailable.HasValue)
            {
                return _isDatabaseAvailable.Value;
            }

            try
            {
                var configuration = new ConfigurationManager();
                // Add configuration sources as needed, e.g. AWS Secrets or Azure Key Vault.
                // Order is important! The last configuration source will override any previous ones.
                configuration.AddJsonFile("appsettings.Test.json");

                // Set up dependency injection with debug and console logging.
                var services = new ServiceCollection();

                // Register the DbContext using the composition extension method.
                services.RegisterDbContext(configuration);

                // Check if the database is available.
                var provider = services.BuildServiceProvider();
                var context = provider.GetRequiredService<SampleDbContext>();
                _isDatabaseAvailable = context.Database.CanConnect();
            }
            catch
            {
                _isDatabaseAvailable = false;
            }

            return _isDatabaseAvailable.Value;
        }
    }
}
