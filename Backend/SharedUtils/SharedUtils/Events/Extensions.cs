using Microsoft.Extensions.DependencyInjection;

namespace SharedUtils.Events;
public static class Extensions
{
    public static IServiceCollection AddIntegrationEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventsQueueService, EventsQueueService>();
        return services;
    }
}
