using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DbScaffold.Tests;

public class IntegrationTestFixture
{
    private readonly IServiceProvider _provider;

    public IntegrationTestFixture()
    {
        var configuration = new ConfigurationManager();
        // Add configuration sources as needed, e.g. AWS Secrets or Azure Key Vault.
        // Order is important! The last configuration source will override any previous ones.
        configuration.AddJsonFile("appsettings.Test.json");

        // Set up dependency injection with debug and console logging.
        var services = new ServiceCollection();
        services.AddLogging(options =>
        {
            options.ClearProviders();
            options.AddConsole();
            options.AddDebug();
        });

        // Register the DbContext using the composition extension method.
        services.RegisterDbContext(configuration);

        // Create a service provider.
        _provider = services.BuildServiceProvider();
    }

    public T GetRequiredService<T>() where T : notnull => _provider.GetRequiredService<T>();
}
