using Microsoft.Extensions.DependencyInjection;
using Modules.UserManagement.Infrastructure.DataAccessLayer;
using Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;
using SharedUtils.Database;

namespace Modules.UserManagement.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddNpgsql<UserManagementDbContext>(UserManagementDbContext.DEFAULT_SCHEMA);
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}
