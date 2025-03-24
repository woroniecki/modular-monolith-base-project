using Microsoft.Extensions.DependencyInjection;

namespace Modules.Core.App;

public static class Extensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}
