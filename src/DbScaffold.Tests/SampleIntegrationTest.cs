namespace DbScaffold.Tests;

// This collection attribute is used to tell the test framework to
// inject the IntegrationTestFixture into the test class.
[Collection("IntegrationTest")]
public class SampleIntegrationTest : IDisposable
{
    private readonly IntegrationTestFixture _fixture;

    // Use class-level private variables to store the ids of
    // the entities you create in the test so that you can
    // clean them up in the Dispose method.

    public SampleIntegrationTest(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        Initialize();
    }

    // Use this method to set up any test data or state.
    private void Initialize()
    {
        var context = _fixture.GetRequiredService<SampleDbContext>();
    }

    // Use this method to clean up any test data or state.
    public void Dispose()
    {
        var context = _fixture.GetRequiredService<SampleDbContext>();
    }

    [Fact]
    public void Test1()
    {
        // Using the fixture, we can access the injected queries and commands.
        // If you don't need them, then you are likely not writing an integration test.        
    }
}
