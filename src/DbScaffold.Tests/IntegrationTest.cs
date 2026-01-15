namespace DbScaffold.Tests;

[CollectionDefinition("IntegrationTest")]
public class IntegrationTest : ICollectionFixture<IntegrationTestFixture>
{
    // This class has no code, and is never created. Its purpose is
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces. All classes in the collection
    // will share all the ICollectionFixture<> implementations.
}
