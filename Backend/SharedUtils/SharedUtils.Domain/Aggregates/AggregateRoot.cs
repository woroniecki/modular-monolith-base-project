using System.ComponentModel.DataAnnotations;
using SharedUtils.Domain.Events;

namespace SharedUtils.Domain.Aggregates;

public class AggregateRoot : IAggregateRoot
{
    public AggregateRoot()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private readonly List<IDomainEvent> _domainEvents = new();

    [Key]
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
