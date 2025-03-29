using Coravel.Invocable;
using MediatR;
using Modules.UserManagement.App.Queries.HealthCheck;

namespace BackgroundTasks.Tasks.UserManagement;
public class TestTask(IMediator mediator) : IInvocable
{
    public async Task Invoke()
    {
        await mediator.Send(new HealthCheckQuery());
    }
}
