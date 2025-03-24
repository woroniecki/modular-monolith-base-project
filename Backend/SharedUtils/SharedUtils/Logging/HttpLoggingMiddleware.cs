using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace SharedUtils.Logging
{
    public class HttpLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpLoggingMiddleware> _logger;

        public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            LogRequest(context);

            await _next.Invoke(context);

            LogResponse(context);
        }

        private void LogRequest(HttpContext context)
        {
            _logger.LogInformation($"Request [{context.Request.Method.ToUpper()}] {context.Request.Path}");
        }

        private void LogResponse(HttpContext context)
        {
            _logger.LogInformation($"Response ({context.Response.StatusCode})");
        }
    }
}