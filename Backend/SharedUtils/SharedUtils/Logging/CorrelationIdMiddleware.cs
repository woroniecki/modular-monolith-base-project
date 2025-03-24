using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace SharedUtils.Logging;
public class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        context.Request.Headers.TryGetValue("CorrelationId", out var correlationIds);
        var correlationId = correlationIds.FirstOrDefault() ?? Guid.NewGuid().ToString();

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next(context);
        }
    }
}
