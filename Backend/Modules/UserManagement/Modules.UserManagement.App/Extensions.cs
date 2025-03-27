using Microsoft.Extensions.DependencyInjection;
using Modules.UserManagement.App.Services.RefreshTokenService;

namespace Modules.UserManagement.App;

public static class Extensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        return services;
    }
}
