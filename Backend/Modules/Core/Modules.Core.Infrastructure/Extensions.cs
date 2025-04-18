using Microsoft.Extensions.DependencyInjection;
using Modules.Core.Infrastructure.DataAccessLayer;
using Modules.Core.Infrastructure.DataAccessLayer.UoT;
using SharedUtils.Database;

namespace Modules.Core.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddNpgsql<CoreDbContext>(CoreDbContext.DEFAULT_SCHEMA);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
