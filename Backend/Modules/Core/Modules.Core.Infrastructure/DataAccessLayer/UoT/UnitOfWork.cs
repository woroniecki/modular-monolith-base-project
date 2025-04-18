
namespace Modules.Core.Infrastructure.DataAccessLayer.UoT;
internal class UnitOfWork(CoreDbContext _context) : IUnitOfWork
{
    public CoreDbContext DbContext => _context;

    public async Task<int> SaveAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
