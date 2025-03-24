using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SharedUtils.Auth;
using SharedUtils.Cqrs;
using SharedUtils.Database;
using SharedUtils.Events;
using SharedUtils.Jwt;
using SharedUtils.Logging;
using SharedUtils.Time;

namespace SharedUtils;

public static class Extensions
{
    private const string ApiTitle = "Project";
    private const string ApiVersion = "v1";

    public static void AddSharedFramework(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddCqrs();
        builder.Services.AddNpgsql(configuration);
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IClock, UtcClock>();
        builder.Services.AddOpenApi();
        builder.Services.AddJwt();
        builder.Services.AddAuth(configuration);
        builder.Services.AddIntegrationEvents();
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} {Level:u3} [{CorrelationId}] {Message}{NewLine}{Exception}")
            .Enrich.FromLogContext();
            loggerConfiguration.ReadFrom.Configuration(context.Configuration);
        });
    }

    public static WebApplication UseSharedFramework(this WebApplication app)
    {
        app.MapOpenApi();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/openapi/v1.json", $"{ApiTitle} {ApiVersion}");
        });
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<HttpLoggingMiddleware>();

        return app;
    }
}
