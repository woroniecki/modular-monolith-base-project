using Microsoft.Extensions.DependencyInjection;
using Modules.UserManagement.App;
using Modules.UserManagement.Domain;
using Modules.UserManagement.Infrastructure;

namespace Modules.UserManagement.API;

public static class Extensions
{
    public static IServiceCollection AddUserManagementModule(this IServiceCollection services)
    {
        services.AddApplicationLayer()
                .AddInfrastructureLayer()
                .AddDomainLayer();

        return services;
    }
}
