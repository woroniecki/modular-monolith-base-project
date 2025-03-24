using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace SharedUtils.Database;
public static class Extensions
{
    private const string SectionName = "npqsql";

    internal static IServiceCollection AddNpgsql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NpgsqlOptions>(configuration.GetSection(SectionName));
        services.AddHostedService<DbContextAppInitializer>();

        return services;
    }

    public static IServiceCollection AddNpgsql<T>(this IServiceCollection services, string schema) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString =
            configuration[$"{SectionName}:{nameof(NpgsqlOptions.ConnectionString)}"]
            + $"Database={schema};";

        services.AddDbContext<T>(options =>
            options.UseNpgsql(
                connectionString,
                o => o.SetPostgresVersion(13, 0))
                      .UseLoggerFactory(new NullLoggerFactory()) //Disable EF Core logging
        );

        //Inject as well the DbContext as a service
        //for transaction pipeline behavior
        services.AddScoped<DbContext>(sp => sp.GetRequiredService<T>());

        return services;
    }
}
