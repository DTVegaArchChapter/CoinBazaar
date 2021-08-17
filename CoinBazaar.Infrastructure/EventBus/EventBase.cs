using System;
using System.Text.Json.Serialization;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class EventBase : IEvent
    {
        public Guid Id { get; set; }

        public Guid AggregateId { get; protected set; }

        public long Version { get; set; }

        public DateTime CreationDate { get; private init; }

        public EventBase()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Version = 0;
        }

        [JsonConstructor]
        public EventBase(Guid id, int version, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
            Version = version;
        }
    }
}
