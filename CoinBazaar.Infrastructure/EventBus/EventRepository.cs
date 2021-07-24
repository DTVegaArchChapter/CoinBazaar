using EventStore.Client;
using System;
using CoinBazaar.Infrastructure.Extensions;
using System.Threading.Tasks;
using CoinBazaar.Infrastructure.Annotations;
using CoinBazaar.Infrastructure.Models;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class EventRepository : IEventRepository
    {
        private readonly EventStoreClient _eventStore;
        private readonly string _streamName;
        public EventRepository(EventStoreClient eventStore, string streamName)
        {
            _eventStore = eventStore;
            _streamName = streamName;
        }

        public string StreamName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
