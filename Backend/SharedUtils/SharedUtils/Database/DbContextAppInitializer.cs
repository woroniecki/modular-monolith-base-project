using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SharedUtils.Database;
internal sealed class DbContextAppInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DbContextAppInitializer> _logger;

    public DbContextAppInitializer(IServiceProvider serviceProvider, ILogger<DbContextAppInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContext in scope.ServiceProvider.GetServices<DbContext>())
        {
            _logger.LogInformation($"Running DB context: {dbContext.GetType().Name}...");
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
