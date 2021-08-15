namespace CoinBazaar.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;

    using CoinBazaar.Infrastructure.EventBus;

    using EventStore.Client;

    public static class DomainResponseHelper
    {
        public static DomainEventResult CreateDomainResponse(Guid aggregateId, bool processStarter, object @event, params KeyValuePair<string, object>[] responseParameters)
        {
            ESMetadata metadata = new ESMetadata(aggregateId, processStarter, DateTime.UtcNow, "DummyUser");

            //Metadata needed for correlations or causations
            return new DomainEventResult()
            {
                AggregateId = aggregateId,
                EventData = new EventData(
                    Uuid.NewUuid(),
                    @event.GetType().AssemblyQualifiedName,
                    Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event)),
                    Encoding.UTF8.GetBytes(JsonSerializer.Serialize(metadata))
                    ),
                ResponseParameters = responseParameters
            };
        }
    }
}
