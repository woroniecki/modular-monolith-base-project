using Modules.UserManagement.Domain.Aggregates;

namespace Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByUsernameAsync(string username);
    Task<Account?> GetByEmailAsync(string email);
    Task AddAsync(Account account);
}
