using SharedUtils.Domain.Events;

namespace SharedUtils.Domain.Aggregates;
public interface IAggregateRoot
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
    public void MarkUpdated();
}
