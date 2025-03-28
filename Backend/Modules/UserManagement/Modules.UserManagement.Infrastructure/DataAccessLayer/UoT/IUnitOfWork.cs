using Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;

namespace Modules.UserManagement.Infrastructure.DataAccessLayer.UoT;
public interface IUnitOfWork
{
    UserManagementDbContext DbContext { get; }
    IAccountRepository Accounts { get; }

    Task<int> SaveAsync();
}
