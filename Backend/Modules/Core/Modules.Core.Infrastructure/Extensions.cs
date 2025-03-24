using Microsoft.Extensions.DependencyInjection;
using Modules.Core.Infrastructure.DataAccessLayer;
using SharedUtils.Database;

namespace Modules.Core.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddNpgsql<CoreDbContext>(CoreDbContext.DEFAULT_SCHEMA);

        return services;
    }
}
