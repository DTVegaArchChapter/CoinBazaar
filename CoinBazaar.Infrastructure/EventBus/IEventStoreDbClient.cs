namespace CoinBazaar.Infrastructure.EventBus
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventStoreDbClient
    {
        Task<(long Version, IEnumerable<IEvent> Events)> ReadEventsAsync(string streamName, Guid aggregateId);

        Task AppendEventAsync(string streamName, IEvent @event);
    }
}