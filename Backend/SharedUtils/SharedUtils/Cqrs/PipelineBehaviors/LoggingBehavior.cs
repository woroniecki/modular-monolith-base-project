using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SharedUtils.Cqrs.PipelineBehaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var requestData = JsonSerializer.Serialize(request);

        _logger.LogInformation("Handling {RequestName} with data: {RequestData}", requestName, requestData);

        try
        {
            var response = await next();

            var responseData = JsonSerializer.Serialize(response);
            _logger.LogInformation("Handled {RequestName} successfully with response: {ResponseData}", requestName, responseData);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling {RequestName} with data: {RequestData}", requestName, requestData);
            throw;  // Ensure the exception is propagated
        }
    }
}