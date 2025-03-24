using MediatR;

namespace Modules.Core.App.Queries.HealthCheck;
public class HealthCheckQuery : IRequest<string>
{
    public HealthCheckQuery()
    {

    }
}
