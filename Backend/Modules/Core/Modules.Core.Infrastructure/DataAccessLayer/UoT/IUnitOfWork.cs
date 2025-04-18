namespace Modules.Core.Infrastructure.DataAccessLayer.UoT;
public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken ct);
}
