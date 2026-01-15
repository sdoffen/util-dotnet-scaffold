namespace DbScaffold;

/// <summary>
/// Options for configuring the database connection.
/// </summary>
public class DatabaseOptions
{
    /// <summary>
    /// The name of the section in the configuration file that contains the database options.
    /// </summary>
    public static readonly string SectionName = "DatabaseOptions";

    /// <summary>
    /// <para>
    /// When true, the change tracker will not track any of the entities that are returned from a LINQ query.
    /// </para>
    /// <para>
    /// If the entity instances are modified, this will not be automatically detected by the change
    /// tracker and DbContext.SaveChanges() will not persist those changes to the database.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The default value is true, which means that the change tracker will not track any of the entities.
    /// </remarks>
    public bool AsNoTracking { get; set; } = true;

    /// <summary>
    /// The name of the database to connect to.
    /// </summary>
    /// <remarks>
    /// This property is required; if it is not set, an exception will be thrown when the DbContext is configured.
    /// </remarks>
    public required string DatabaseName { get; set; }

    /// <summary>
    /// <para>
    /// Configures the context to use the default retrying Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy.
    /// This is useful for transient fault handling, such as when using SQL Server or PostgreSQL.
    /// </para>
    /// <para>
    /// When set to true, the context will automatically retry failed database operations
    /// a specified number of times before throwing an exception.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The default value is true, which means that the context will enable retry on failure.
    /// </remarks>
    public bool EnableRetryOnFailure { get; set; } = true;

    /// <summary>
    /// When <see cref="EnableRetryOnFailure"/> is true, these values represent additional SQL error
    /// codes that should trigger retries. Useful for adding database-specific error codes.
    /// </summary>
    /// <remarks>
    /// The default value is null, which means that no additional error codes will be added.
    /// </remarks>
    public ICollection<string>? ErrorCodesToAdd { get; set; }

    /// <summary>
    /// The hostname or IP address of the server to connect to.
    /// </summary>
    /// <remarks>
    /// This property is required; if it is not set, an exception will be thrown when the DbContext is configured.
    /// </remarks>
    public required string Host { get; set; }

    /// <summary>
    /// When <see cref="EnableRetryOnFailure"/> is true, the value represents the maximum number of
    //  retry attempts before failing the operation. If set to 0, no retries will be attempted.
    /// <para>A higher value increases resilience, but may delay failure detection.</para>
    /// </summary>
    /// <remarks>
    /// The default value is 5, which means that the context will retry the operation up to 5 times.
    /// </remarks>
    public int MaxRetryCount { get; set; } = 5;

    /// <summary>
    /// When <see cref="EnableRetryOnFailure"/> is true, the maximum delay in seconds between retry attempts.
    /// Setting a longer delay can help handle temporary outages.
    /// </summary>
    /// <remarks>
    /// The default value is 30 seconds, which means that the context will wait up to 30 seconds
    /// between retry attempts.
    /// </remarks>
    public int MaxRetryDelaySeconds { get; set; } = 30;

    /// <summary>
    /// The assembly where the migrations are maintained for this context.
    /// </summary>
    /// <remarks>
    /// This property is optional. If not set, the migrations will be assumed to be in the same assembly as the DbContext.
    /// </remarks>
    public string? MigrationsAssembly { get; set; }

    /// <summary>
    /// The password to use when connecting to the database.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The TCP/IP port of the database server.
    /// </summary>
    /// <remarks>
    /// This property is required; if it is not set, an exception will be thrown when the DbContext is configured.
    /// </remarks>
    public required int Port { get; set; }

    /// <summary>
    /// <para>
    /// Indicates whether to use DbContext pooling.
    /// </para>
    /// <para>
    /// When set to true, the DbContext will be pooled, which can improve performance
    /// by reusing existing instances instead of creating new ones.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The default value is true, which means that DbContext pooling is enabled.
    /// </remarks>
    public bool UseDbContextPooling { get; set; } = true;

    /// <summary>
    /// <para>
    /// Indicates whether to use the migrations assembly for the DbContext.
    /// </para>
    /// <para>
    /// When set to true, the DbContext will use the specified MigrationsAssembly
    /// to look for migrations. This is useful when the migrations are located in a
    /// different assembly than the DbContext itself.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The default value is false, which means that the DbContext will not use a separate migrations assembly.
    /// </remarks>
    public bool UseMigrationsAssembly { get; set; } = false;

    /// <summary>
    /// The username to use when connecting to the database.
    /// </summary>
    /// <remarks>
    /// This property is required; if it is not set, an exception will be thrown when the DbContext is configured.
    /// </remarks>
    public required string Username { get; set; }
}
