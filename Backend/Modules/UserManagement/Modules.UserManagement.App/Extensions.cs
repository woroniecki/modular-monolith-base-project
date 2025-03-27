using Microsoft.Extensions.DependencyInjection;
using Modules.UserManagement.App.Services.Password;
using Modules.UserManagement.App.Services.RefreshToken;

namespace Modules.UserManagement.App;

public static class Extensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        return services;
    }
}
