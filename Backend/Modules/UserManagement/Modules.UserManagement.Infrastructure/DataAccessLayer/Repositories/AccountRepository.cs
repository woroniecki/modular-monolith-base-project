using Microsoft.EntityFrameworkCore;
using Modules.UserManagement.Domain.Aggregates;

namespace Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly UserManagementDbContext _context;

    public AccountRepository(UserManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByUsernameAsync(string username)
    {
        return await _context.Accounts.SingleOrDefaultAsync(a => a.Username == username);
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
    }

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }
}
