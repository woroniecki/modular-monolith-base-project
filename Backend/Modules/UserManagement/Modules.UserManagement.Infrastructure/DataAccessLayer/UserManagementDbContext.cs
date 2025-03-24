using Microsoft.EntityFrameworkCore;
using Modules.UserManagement.Domain.Aggregates;
using SharedUtils.Database;

//Run from bootstrapper project folder
//dotnet ef migrations add MigrationName --project ../Modules/UserManagement/Modules.UserManagement.Infrastructure --context UserManagementDbContext
namespace Modules.UserManagement.Infrastructure.DataAccessLayer;
public sealed class UserManagementDbContext : BaseDbContext
{
    public const string DEFAULT_SCHEMA = "user_management_db";

    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
    : base(options) { }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
