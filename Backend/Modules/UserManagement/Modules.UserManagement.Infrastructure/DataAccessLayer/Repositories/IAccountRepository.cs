using Modules.UserManagement.Domain.Aggregates.Account;

namespace Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByUsernameAsync(string username);
    Task<Account?> GetByEmailAsync(string email);
    Task<Account?> GetByRefreshTokenAsync(string refreshToken);
    Task AddAsync(Account account);

    Task UpdateAsync(Account account);
}
