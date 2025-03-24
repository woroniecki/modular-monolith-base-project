namespace SharedUtils.Events;
public interface IEventsQueueService
{
    void Add(IntegrationEvent evt);
    IEnumerable<IntegrationEvent> Flush();
}
