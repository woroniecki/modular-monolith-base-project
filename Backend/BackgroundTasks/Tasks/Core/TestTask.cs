using Coravel.Invocable;
using MediatR;
using Modules.Core.App.Queries.Test;

namespace BackgroundTasks.Tasks.Core;
public class TestTask(IMediator mediator) : IInvocable
{
    public async Task Invoke()
    {
        await mediator.Send(new TestQuery());
    }
}
