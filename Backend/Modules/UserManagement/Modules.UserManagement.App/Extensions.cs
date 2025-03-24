using Microsoft.Extensions.DependencyInjection;

namespace Modules.UserManagement.App;

public static class Extensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}
