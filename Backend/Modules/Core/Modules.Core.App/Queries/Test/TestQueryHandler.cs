using MediatR;
using Microsoft.Extensions.Logging;

namespace Modules.Core.App.Queries.Test;
internal sealed class TestQueryHandler(
    ILogger<TestQueryHandler> logger
    ) : IRequestHandler<TestQuery, string>
{
    public async Task<string> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Test");
        return await Task.FromResult("Test");
    }
}
