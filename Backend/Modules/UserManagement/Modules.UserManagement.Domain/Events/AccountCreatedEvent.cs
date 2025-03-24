using SharedUtils.Domain.Events;

public record AccountCreatedEvent(Guid accountId) : IDomainEvent;
