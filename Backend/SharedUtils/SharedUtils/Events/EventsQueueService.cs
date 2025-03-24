using System.Collections.Concurrent;

namespace SharedUtils.Events;

/// <summary>
/// Injected as a scoped so life time is limited to api call
/// </summary>
public class EventsQueueService : IEventsQueueService
{
    private readonly ConcurrentQueue<IntegrationEvent> _queue = new();

    public void Add(IntegrationEvent evt)
    {
        _queue.Enqueue(evt);
    }

    public IEnumerable<IntegrationEvent> Flush()
    {
        var events = new List<IntegrationEvent>();
        while (_queue.TryDequeue(out var dequeuedEvent))
        {
            events.Add(dequeuedEvent);
            yield return dequeuedEvent;
        }
    }
}
