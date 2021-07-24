using CoinBazaar.Infrastructure.EventBus;
using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.Helpers
{
    public static class DomainResponseHelper
    {
        public static async Task<DomainEventResult> CreateDomainResponse(Guid aggregateId, IEvent @event, params KeyValuePair<string, object>[] responseParameters)
        {
            //TODO: need Metadata for later correlations or causations
            return new DomainEventResult()
            {
                AggregateId = aggregateId,
                EventData = new EventData(
                    Uuid.NewUuid(),
                    @event.GetType().Name,
                    JsonSerializer.SerializeToUtf8Bytes(@event)
                    ),
                ResponseParameters = responseParameters
            };
        }
    }
}
