using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedUtils.Cqrs.PipelineBehaviors;

namespace SharedUtils.Cqrs;
internal static class Extensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(GetAllAssemblies().ToArray()));
        services.AddLoggingBehaviour();
        services.AddTransactionAndDomainEventsBehavior();

        return services;
    }

    private static IEnumerable<Assembly> GetAllAssemblies()
    {
        var rootAssembly = Assembly.GetEntryAssembly();

        var visited = new HashSet<string>();
        var queue = new Queue<Assembly>();

        queue.Enqueue(rootAssembly);

        while (queue.Any())
        {
            var assembly = queue.Dequeue();
            visited.Add(assembly.FullName);

            var references = assembly.GetReferencedAssemblies();
            foreach (var reference in references)
            {
                if (!visited.Contains(reference.FullName))
                {
                    queue.Enqueue(Assembly.Load(reference));
                }
            }

            yield return assembly;
        }
    }

    public static void AddTransactionAndDomainEventsBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionAndEventsBehavior<,>));
    }

    public static void AddLoggingBehaviour(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}
