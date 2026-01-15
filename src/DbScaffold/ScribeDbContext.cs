using Microsoft.EntityFrameworkCore;

namespace DbScaffold;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

    // Define your DbSets here
    // public DbSet<YourEntity> YourEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entities here

        /*
        * How To Enable Soft Deletes
        *
        * Ensure that your entity has a public boolean property specifically for flagging
        * records as deleted. This property can be named whatever you want. In this
        * example, we will use "IsDeleted".
        *
        * In the OnModelCreating method, add a query filter to the modelBuilder for the
        * entity that you want to enable soft deletes for.
        *
        * modelBuilder.Entity<YourEntity>()
        *     .HasQueryFilter(e => !e.IsDeleted);
        *
        * Entities can then be marked as deleted by setting the IsDeleted property to true using
        * the the ExecuteUpdateAsync method in the command handler. This will eliminate the need
        * to load the entity into memory before marking it as deleted.
        *
        * await context.YourEntities
        *     .Where(e => e.Id == id)
        *     .ExecuteUpdateAsync(e => e.SetProperty(x => x.IsDeleted, true));
        *
        * To include soft-deleted entities in your queries, you can use the IgnoreQueryFilters method
        * in your query handlers.
        *
        * await context.YourEntities
        *     .IgnoreQueryFilters()
        *     .ToListAsync();
        */
    }
}
