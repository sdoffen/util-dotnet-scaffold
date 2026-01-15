# DbScaffold

Generate a new DbContext and models from an existing database using the command:

```
dotnet ef dbcontext scaffold "Host=localhost;Port=5433;Database=scaffold;Username=postgres;Password=StrongPassword123!" Npgsql.EntityFrameworkCore.PostgreSQL --project DbScaffold --startup-project DbScaffold.Design --output-dir Models --context SampleDbContext --schema public
```

| Property          | Sample Value                                                                               |
|-------------------|--------------------------------------------------------------------------------------------|
| Connection String | `Host=localhost;Port=5433;Database=scaffold;Username=postgres;Password=StrongPassword123!` |
| Provider          | `Npgsql.EntityFrameworkCore.PostgreSQL`                                                    |
| Project           | `DbScaffold`                                                                               |
| Startup Project   | `DbScaffold.Design`                                                                        |
| Output Directory  | `Models`                                                                                   |
| DbContext         | `SampleDbContext`                                                                          |
| Schema            | `public`                                                                                   |

