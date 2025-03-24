using Microsoft.Extensions.DependencyInjection;
using Modules.Core.App;
using Modules.Core.Domain;
using Modules.Core.Infrastructure;

namespace Modules.Core.API;

public static class Extensions
{
    public static IServiceCollection AddCoreModule(this IServiceCollection services)
    {
        services.AddApplicationLayer()
                .AddInfrastructureLayer()
                .AddDomainLayer();

        return services;
    }
}
