using Microsoft.EntityFrameworkCore;
using SharedUtils.Domain.Aggregates;

namespace SharedUtils.Database;

public abstract class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options)
    : base(options) { }

    public override int SaveChanges()
    {
        UpdateAggregateRootEntities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAggregateRootEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAggregateRootEntities()
    {
        var entries = ChangeTracker.Entries<IAggregateRoot>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.MarkUpdated();
        }
    }
}
