namespace CoinBazaar.Infrastructure.EventBus
{
    using System;
    using System.Threading.Tasks;

    using CoinBazaar.Infrastructure.Aggregates;
    using CoinBazaar.Infrastructure.Annotations;

    public sealed class EventSourceRepository : IEventSourceRepository
    {
        private readonly IEventStoreDbClient _client;

        public EventSourceRepository(IEventStoreDbClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Task<TAggregate> FindByIdAsync<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregateRoot, new()
        {
            if (aggregateId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(aggregateId));
            }

            return FindByIdInternalAsync<TAggregate>(aggregateId);
        }

        public async Task SaveAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot
        {
            var aggregateVersion = aggregate.Version;
            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                @event.Version = aggregateVersion++;
                await _client.AppendEventAsync(StreamNameAttribute.GetStreamName<TAggregate>(), @event).ConfigureAwait(false);
            }

            aggregate.MarkChangesAsCommitted();
        }

        private async Task<TAggregate> FindByIdInternalAsync<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregateRoot, new()
        {
            var aggregate = new TAggregate();
            var result = await _client.ReadEventsAsync(StreamNameAttribute.GetStreamName<TAggregate>(), aggregateId).ConfigureAwait(false);
            if (result == default)
            {
                return default;
            }

            aggregate.LoadFromHistory(result.Version, result.Events);
            return aggregate;
        }
    }
}
