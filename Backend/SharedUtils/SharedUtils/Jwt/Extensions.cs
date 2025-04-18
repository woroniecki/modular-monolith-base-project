using Microsoft.Extensions.DependencyInjection;
using SharedUtils.Jwt.CurrentUser;

namespace SharedUtils.Jwt;
public static class Extensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
