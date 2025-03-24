using Microsoft.Extensions.DependencyInjection;

namespace Modules.Core.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services)
    {
        return services;
    }
}
