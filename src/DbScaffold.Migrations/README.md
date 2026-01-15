# DbScaffold.Migrations

A quickstart guide to using [Entity Framework Core Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) with this project.

> [!IMPORTANT]
> When setting up the EF Core project for the first time, be sure to first follow the steps for [setting up migrations](./SETUP.MD) using an external project. Otherwise the commands below my fail or produce unexpected results.

## Target Database

The migration commands will target the database specified in the `DatabaseOptions` section of the [`appsettings.Design.json`](../DbScaffold.Design/appsettings.Design.json) file.

## Developer Setup

In order to run and manage migrations using Entity Framework Core and this project, you will need to globally install or updated the dotnet [Entity Framework Core .NET Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) using the appropriate command(s).

```bash
# Install
dotnet tool install --global dotnet-ef

# Update
dotnet tool update --global dotnet-ef

# Verify Installation
dotnet ef
```

## Required CLI Options

The CLI commands refer to a _project_ and a _startup project_.

- The _project_ is also known as the _target project_ because it's where the commands add or remove files. By default, the project in the current directory is the target project. We will specify a different project as target project by using the `--project` option, because we want to [store migrations in a different project](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli) than the one containing our `DbContext`.

- The _startup project_ is the one that the tools build and run. The tools have to execute application code at design time to get information about the project, such as the database connection string and the configuration of the model. By default, the project in the current directory is the startup project. You can specify a different project as startup project by using the `--startup-project` option.

| Parameter           | Value                        |
|---------------------|------------------------------|
| `--project`         | DbScaffold.Default.Migrations |
| `--startup-project` | DbScaffold.Default.Design     |

> [!IMPORTANT]
> All commands in the examples below are assumed to be running in the root folder of the project unless otherwise specified.

## Common Migration Commands

This list of common migration commands is not exhaustive. See the [documentation](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) for complete details.

### List Available Migrations

To display a list of available migrations:

```bash
dotnet ef migrations list --project ./src/DbScaffold.Migrations --startup-project ./src/DbScaffold.Design
```

### Check For Pending Changes

To determine if there are any model changes that need to have a migration created:

```bash
dotnet ef migrations has-pending-model-changes --project ./src/DbScaffold.Migrations --startup-project ./src/DbScaffold.Design
```

### Create a New Migration

> [!TIP]
> It is recommended to use Pascal case when naming migrations, to avoid the static analysis warning [CS8981](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/warning-waves#cs8981---the-type-name-only-contains-lower-cased-ascii-characters). Nevertheless, this warning is disabled in the project file of the migrations project.

Create a new migration using the desired name. In the example, the migration is named `MyMigration`.

```bash
dotnet ef migrations add MyMigration --project ./src/DbScaffold.Migrations --startup-project ./src/DbScaffold.Design
```

### Apply Migrations to Lower Environments

Update the target database to the latest migrations:

```bash
dotnet ef database update --project ./src/DbScaffold.Migrations --startup-project ./src/DbScaffold.Design
```

> [!WARNING]
> This command should only be used to apply migrations to development (lower) environments.

### Remove Migrations from Lower Environments

Remove the last migration that was applied, rolling back the changes from that migration:

```bash
dotnet ef migrations remove --project ./src/DbScaffold.Migrations --startup-project ./src/DbScaffold.Design
```

### Apply Migrations to Higher Environments

Updates to production (higher) environments should be done via scripts. Script generation accepts the following two arguments to indicate which range of migrations should be generated:
- The from migration should be the last migration applied to the database before running the script. If no migrations have been applied, specify 0 (this is the default).
- The to migration is the last migration that will be applied to the database after running the script. This defaults to the last migration in your project.
- If no starting and ending values are provided, then the command generates a SQL script from a blank database to the latest migration.

| Flag           | Description                                  |
|----------------|----------------------------------------------|
| `--output`     | Specified the output file for the migration. |
| `--idempotent` | Generates a script that will check which migrations have already been applied, and only apply the missing ones. |

Example:

```bash
dotnet ef migrations script StartingMigration EndingMigration --idempotent --output ./migration.sql --project ./src/DbScaffold.Migrations --startup-project ./src/DbScaffold.Design
```

The script can then be applied to the target database from the command line:

```
psql -h <host> -U <username> -d <database_name> -f migration.sql
```

### Remove Migrations from Higher Environments

Using the same command used to generate a migration script, if our starting migration (from) is newer than the ending migration (to), then a rollback migration script is generated. Rollback scripts can be applied to the database using the same tools that were used to apply migrations.

