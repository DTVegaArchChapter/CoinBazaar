namespace CoinBazaar.Infrastructure.EventBus
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using EventStore.Client;

    public sealed class EventStoreDbClient : IEventStoreDbClient
    {
        private readonly EventStoreClient _eventStore;
        private const long MaxEventCount = 99999999999;

        public EventStoreDbClient(EventStoreClient eventStore)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public async Task<(long Version, IEnumerable<IEvent> Events)> ReadEventsAsync(string streamName, Guid aggregateId)
        {
            var persistedEvents = new List<IEvent>();
            long aggregateVersion = -1;

            var events = _eventStore.ReadStreamAsync(
                Direction.Forwards,
                $"{streamName}-{aggregateId}",
                StreamPosition.Start,
                MaxEventCount,
                null,
                true);

            if (await events.ReadState.ConfigureAwait(false) == ReadState.StreamNotFound)
            {
                return default;
            }

            await foreach (var e in events)
            {
                persistedEvents.Add(DeserializeEvent(e.Event.EventType, e.Event.Data));
                aggregateVersion++;
            }

            return (aggregateVersion, persistedEvents);
        }

        public async Task AppendEventAsync(string streamName, IEvent @event)
        {
            var eventData = new EventData(
                Uuid.NewUuid(),
                @event.GetType().AssemblyQualifiedName,
                Serialize(@event),
                Serialize(new EventSourceMetadata(@event.AggregateId, @event is IProcessStarterEvent, DateTime.UtcNow, "DummyUser")));

            await _eventStore.AppendToStreamAsync(
                $"{streamName}-{@event.AggregateId}",
                StreamState.Any,
                new[] { eventData }).ConfigureAwait(false);
        }

        private static byte[] Serialize(object @event)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
        }

        private static IEvent DeserializeEvent(string eventType, ReadOnlyMemory<byte> data)
        {
            return (IEvent)JsonSerializer.Deserialize(Encoding.UTF8.GetString(data.Span), Type.GetType(eventType));
        }
    }
}