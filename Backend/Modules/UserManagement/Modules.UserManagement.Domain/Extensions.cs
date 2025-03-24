using Microsoft.Extensions.DependencyInjection;

namespace Modules.UserManagement.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services)
    {
        return services;
    }
}
