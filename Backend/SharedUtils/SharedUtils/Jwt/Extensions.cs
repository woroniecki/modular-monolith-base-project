using Microsoft.Extensions.DependencyInjection;

namespace SharedUtils.Jwt;
public static class Extensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
