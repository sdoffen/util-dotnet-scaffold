# Setting Up Migrations

_Adapted from [Using a Separate Migration Project](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli) article._

In order to store our migrations in the `DbScaffold.Migrations` project instead of the default `DbScaffold` project, we need to follow these steps when creating the very first migration.

## Create the Initial Migration

As soon as there are models to add to the database, we can create our first migration using the command:

```
dotnet ef migrations add InitialMigration --project ./src/DbScaffold --startup-project ./src/DbScaffold.Design
```

This will add a `Migrations` folder in the `DbScaffold` project that contains the initial migration and a model snapshot.

```
src/
├── DbScaffold/
|   └── Migrations/
|       ├── YYYYMMDDhhmmss_InitialMigration.cs
|       ├── YYYYMMDDhhmmss_InitialMigration.cs
|       └── SampleDbContextModelSnapshot.cs
├── DbScaffold.Design/
├── DbScaffold.Migrations/
└── DbScaffold.Tests/
```

> [!NOTE]
> `YYYYMMDDhhmmss` in the above file name will actually be a timestamp for when the migration was created, representing the four digit year, and 2 digits each for month, day, hour, minute and second.

## Move the Migrations Folder

Move the entire `Migrations` folder generated in the first step into the `DbScaffold.Migrations` folder.

```
src/
├── DbScaffold/
├── DbScaffold.Design/
├── DbScaffold.Migrations/
|   └── Migrations/
|       ├── YYYYMMDDhhmmss_InitialMigration.cs
|       ├── YYYYMMDDhhmmss_InitialMigration.cs
|       └── SampleDbContextModelSnapshot.cs
└── DbScaffold.Tests/
```

## Modify the Project Configuration

Modify the [`DbScaffold.Design.csproj`](../DbScaffold.Design/DbScaffold.Design.csproj) file to reference `DbScaffold.Migrations` instead of `DbScaffold` directly.

```csproj
  <ItemGroup>
    <ProjectReference Include="..\DbScaffold.Migrations\DbScaffold.Migrations.csproj" />
  </ItemGroup>
```

The [appsettings.Design.json](../DbScaffold.Design/appsettings.Design.json) file in the `DbScaffold.Design` project should be modified by setting the `DatabaseOptions.UseMigrationsAssembly` property to true.

```json
{
  "DatabaseOptions": {
    "MigrationsAssembly": "DbScaffold.Migrations",
    "UseMigrationsAssembly": true
  }
}
```

Also ensure that the value for `DatabaseOptions.MigrationsAssembly` correctly points to the assembly that now contains your migrations.

> [!NOTE]
> While the configuration for other projects (e.g. Tests or WebApi) can also be updated with the values specified above, this is not necessary, as only the `DbScaffold.Design` project will be running migrations.

## Clean Up

Once these instructions have been completed, this document can (and should) be deleted, as well as the section in the [README.md](./README.md) that refers to it.