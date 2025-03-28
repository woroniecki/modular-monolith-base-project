using Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;

namespace Modules.UserManagement.Infrastructure.DataAccessLayer.UoT;
internal class UnitOfWork(UserManagementDbContext _context) : IUnitOfWork
{
    private IAccountRepository _accounts;

    public UserManagementDbContext DbContext => _context;
    public IAccountRepository Accounts => _accounts ??= new AccountRepository(_context);

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
