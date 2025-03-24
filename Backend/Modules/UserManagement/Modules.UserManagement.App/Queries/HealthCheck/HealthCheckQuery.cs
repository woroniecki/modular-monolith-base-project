using MediatR;

namespace Modules.UserManagement.App.Queries.HealthCheck;
public class HealthCheckQuery : IRequest<string>
{
    public HealthCheckQuery()
    {

    }
}
