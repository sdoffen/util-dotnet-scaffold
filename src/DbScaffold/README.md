# DbScaffold

This project implements the abstractions from _Your.Interfaces.Project_. The DbContext implementation contained in this package is not intended to be available outside of this project, but rather to be consumed by the interface implementations.

## Related Project Structure

DbScaffold is implemented using several projects in order separate the concerns of interface implementation, design time services, database migrations and integration testing. The project structure and dependency graph are as follows:

| Project              | Purpose                                                |
|----------------------|--------------------------------------------------------|
| DbScaffold            | Interface implementations, as well as the DbContext    |
| DbScaffold.Design     | Executable project used for database design operations |
| DbScaffold.Migrations | Database migrations, [see documentation][migration]    |
| DbScaffold.Test       | Integration tests for DbScaffold implementations        |

[migration]: ../DbScaffold.Migrations/README.md

### Dependency Graph

```mermaid
flowchart BT
  A["DbScaffold"]
  B["DbScaffold.Migrations"]
  C["DbScaffold.Tests"]
  D["DbScaffold.Design"]
  B --> A
  C --> A
  D --> B
```

> Flowchart generated using [Mermaid Flowcharts](https://github.blog/developer-skills/github/include-diagrams-markdown-files-mermaid/).

## Project Organization

Use the following guidelines to implement repository interfaces.

- Implement each repository interface in its own directory.
- Each directory will contain:
  - subdirectories for `Commands` and `Queries`
    - Each command and query will have an interface and an implementation
    - The only methods defined on commands and queries will be named `ExecuteAsync`
    - Multiple `ExecuteAsync` methods may be defined
    - An instance of `ILogger<T>` and `IDbContextFactory<SampleDbContext>` will be injected into each command and query
    - It is the responsibility of the command or query to catch database exceptions and wrap them in domain exceptions; the layers that consume the interfaces should not catch exceptions specific to the database provider
  - an interface implementation
    - Each interface implementation will have all of the command and query interfaces injected into it.
  - a static `CompositionExtensions` class
    - Interface implementations and command and query class will be registered in the directory level `CompositionExtensions` as a singleton
- Unit tests should exist to ensure that all interfaces are registered with the container
- Unit tests should exist to ensure that the repositories correctly wrap the commands and queries
- Integration tests should exist for each command and query
  - Each test should implement the `IDisposable` interface
  - Data needed to execute the test should be created using an `Initialize` method
  - Data created to execute the tests should be removed when the test is complete using the `Dispose` method.

An example using an `IPersonRepository` interface might therefore look like this:

```
DbScaffold/
├── Person/
|   ├── Commands
│   |    └── IPersonDeleteCommand.cs
│   |    └── IPersonInsertCommand.cs
│   |    └── IPersonUpdateCommand.cs
│   |    └── PersonDeleteCommand.cs
│   |    └── PersonInsertCommand.cs
│   |    └── PersonUpdateCommand.cs
|   ├── Queries
│   |    └── IPersonGetAllQuery.cs
│   |    └── IPersonGetByIdQuery.cs
│   |    └── PersonGetAllQuery.cs
│   |    └── PersonGetByIdQuery.cs
│   └── CompositionExtensions.cs
│   └── PersonRepository.cs
├── CompositionExtensions.cs
├── DatabaseOptions.cs
└── SampleDbContext.cs
```

## Docker Container

This project template includes a docker container running a database instance. The test project is already configured to use the database instance in this docker container. With Docker running locally, start the container by executing the following from the command line:

```
docker compose up -d
```

You can tear down both the services and the associated volumes by running:

```
docker compose down -v
```

Connect to the database in the container using a client application like [DBeaver](https://dbeaver.io/) or [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio) using the following settings:

| Property | Value     |
|----------|-----------|
| Host     | localhost |
| Database | scaffold |
| Port     | 5433 |
| Username | postgres |
| Password | StrongPassword123! |