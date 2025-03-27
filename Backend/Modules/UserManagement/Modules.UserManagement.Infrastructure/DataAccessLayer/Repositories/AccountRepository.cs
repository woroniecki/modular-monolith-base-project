using Microsoft.EntityFrameworkCore;
using Modules.UserManagement.Domain.Aggregates.Account;

namespace Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;

internal class AccountRepository : IAccountRepository
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

    public async Task<Account?> GetByRefreshTokenAsync(string hashedRefreshToken)
    {
        return await _context.Accounts
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.HashedToken == hashedRefreshToken && !rt.IsRevoked));
    }

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
    }
}
