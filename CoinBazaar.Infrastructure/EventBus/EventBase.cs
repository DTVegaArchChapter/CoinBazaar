using System;
using System.Text.Json.Serialization;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class EventBase : IEvent
    {
        [JsonInclude]
        public Guid Id { get; set; }

        [JsonInclude]
        public int Version { get; set; }

        [JsonInclude]
        public DateTime CreationDate { get; private init; }

        [JsonConstructor]
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
