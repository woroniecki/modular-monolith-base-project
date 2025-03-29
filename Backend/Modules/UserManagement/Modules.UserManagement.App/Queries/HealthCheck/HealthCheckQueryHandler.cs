using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.UserManagement.Infrastructure.DataAccessLayer.UoT;

namespace Modules.UserManagement.App.Queries.HealthCheck;

public class HealthCheckQueryHandler(IUnitOfWork _uot) : IRequestHandler<HealthCheckQuery, string>
{
    public async Task<string> Handle(HealthCheckQuery request, CancellationToken cancellationToken)
    {
        var acc = await _uot.DbContext.Accounts.Where(x => true).FirstOrDefaultAsync();
        return $"Check database connection: {acc.Id}";
    }
}