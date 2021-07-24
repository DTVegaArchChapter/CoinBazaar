using EventStore.Client;
using System;
using System.Collections.Generic;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class DomainEventResult
    {
        public Guid AggregateId { get; set; }
        public IList<KeyValuePair<string, object>> ResponseParameters { get; set; }
        public EventData EventData { get; set; }
    }
}
