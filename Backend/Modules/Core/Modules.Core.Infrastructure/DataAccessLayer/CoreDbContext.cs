using Microsoft.EntityFrameworkCore;
using SharedUtils.Database;

//Run from bootstrapper project folder
//dotnet ef migrations add MigrationName --project ../Modules/Core/Modules.Core.Infrastructure --context CoreDbContext
namespace Modules.Core.Infrastructure.DataAccessLayer;
public sealed class CoreDbContext : BaseDbContext
{
    public const string DEFAULT_SCHEMA = "core_db";

    public CoreDbContext(DbContextOptions<CoreDbContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
