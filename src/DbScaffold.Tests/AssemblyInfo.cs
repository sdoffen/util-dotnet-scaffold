/*
* This attribute specifies that the assembly is an integration test assembly.
* It can be used by test runners or other tools to categorize or filter tests.
*
* For example, to run all tests except integration tests:
*   dotnet test --filter Category!=Integration
*
* To run only integration tests:
*   dotnet test --filter Category=Integration
*/
[assembly: AssemblyTrait("Category", "Integration")]
