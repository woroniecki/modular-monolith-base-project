using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

    /// <summary>
    /// This method is used for api and suppose to have only services that are used in api
    /// </summary>
    public static void AddSharedFrameworkApi(this IServiceCollection services, IHostBuilder host, IConfiguration configuration)
    {
        services.AddSharedFramework(configuration);
        services.AddControllers();
        services.AddOpenApi();
        services.AddAuth(configuration);
        services.AddHostedService<DbContextAppInitializer>();//migrations run only from api start
        host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} {Level:u3} [{CorrelationId}] {Message}{NewLine}{Exception}")
            .Enrich.FromLogContext();
            loggerConfiguration.ReadFrom.Configuration(context.Configuration);
        });
        services.AddHttpContextAccessor();
    }

    public static void AddSharedFrameworkBgTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedFramework(configuration);
    }

    private static void AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwt();
        services.AddNpgsql(configuration);
        services.AddCqrs();
        services.AddSingleton<IClock, UtcClock>();
        services.AddIntegrationEvents();
    }

    public static WebApplication UseApiSharedFramework(this WebApplication app)
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
