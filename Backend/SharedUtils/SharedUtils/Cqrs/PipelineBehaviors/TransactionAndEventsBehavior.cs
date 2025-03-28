using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedUtils.Domain.Aggregates;
using SharedUtils.Events;

namespace SharedUtils.Cqrs.PipelineBehaviors;

/// <summary>
/// It is executed only for commands and it wraps the execution in a transaction.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class TransactionAndEventsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<DbContext> _dbContexts;
    private readonly IMediator _mediator;
    private readonly IEventsQueueService _eventsQueueService;

    public TransactionAndEventsBehavior(
        IEnumerable<DbContext> dbContexts,
        IMediator mediator,
        IEventsQueueService eventsQueueService)
    {
        _dbContexts = dbContexts ?? throw new ArgumentNullException(nameof(dbContexts));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _eventsQueueService = eventsQueueService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!typeof(TRequest).Name.EndsWith("Command"))
        {
            return await next();
        }

        var requestModuleName = GetModuleName(typeof(TRequest).Assembly.GetName().Name);

        var dbContext = _dbContexts
            .Where(db => requestModuleName == GetModuleName(db.GetType().Assembly.GetName().Name))
            .First();

        using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await next();

            await transaction.CommitAsync(cancellationToken);

            await PublishDomainEvents(dbContext, cancellationToken);

            await PublishIntegrationEvents(cancellationToken);

            return response;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    private async Task PublishIntegrationEvents(CancellationToken cancellationToken)
    {
        foreach (var integrationEvent in _eventsQueueService.Flush())
        {
            await _mediator.Publish(integrationEvent, cancellationToken);
        }
    }

    private async Task PublishDomainEvents(DbContext dbContext, CancellationToken cancellationToken)
    {
        var aggregateRoots = dbContext.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        var domainEvents = aggregateRoots
            .SelectMany(e => e.DomainEvents)
            .ToList();

        aggregateRoots.ForEach(e => e.ClearDomainEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            });

        await Task.WhenAll(tasks);
    }

    private string GetModuleName(string assemblyName)
    {
        var parts = assemblyName.Split('.');
        if (parts.Length >= 2)
        {
            return $"{parts[1]}";
        }
        return assemblyName;
    }
}
