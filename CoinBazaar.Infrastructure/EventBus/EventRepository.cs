using CoinBazaar.Infrastructure.Models;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class EventRepository : IEventRepository
    {
        private readonly EventStoreClient _eventStore;
        private readonly string _streamName;

        private const long maxEventCount = 99999999999;

        public EventRepository(EventStoreClient eventStore, string streamName)
        {
            _eventStore = eventStore;
            _streamName = streamName;
        }

        public async Task<List<ResolvedEventDTO>> GetAllEvents(Guid aggregateId)
        {
            var events = _eventStore.ReadStreamAsync(Direction.Forwards, $"{_streamName}-{aggregateId}", StreamPosition.Start, maxEventCount, null, true);

            List<ResolvedEventDTO> eventDtos = new List<ResolvedEventDTO>();

            await foreach (ResolvedEvent @event in events)
            {
                eventDtos.Add(new ResolvedEventDTO()
                {
                    Created = @event.Event.Created,
                    Data = @event.Event.Data,
                    EventId = @event.Event.EventId.ToGuid(),
                    EventNumber = @event.Event.EventNumber,
                    EventStreamId = @event.Event.EventStreamId,
                    EventType = @event.Event.EventType,
                    Metadata = @event.Event.Metadata
                });

                if (events.ReadState.IsCompleted)
                {
                    break;
                }
            }

            return eventDtos;
        }

        public async Task<DomainCommandResponse> Publish(DomainEventResult domainEventResult)
        {
            try
            {
                var result = await _eventStore.AppendToStreamAsync(
                    $"{_streamName}-{domainEventResult.AggregateId}",
                    StreamState.Any,
                    new[] { domainEventResult.EventData }
                );

                return new DomainCommandResponse() { AggregateId = domainEventResult.AggregateId, EventId = domainEventResult.EventData.EventId.ToGuid(), CreationDate = DateTime.UtcNow };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
