using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class ResolvedEventDTO
    {
        public DateTime Created { get; set; }
        public ReadOnlyMemory<byte> Data { get; set; }
        public Guid EventId { get; set; }
        public StreamPosition EventNumber { get; set; }
        public string EventStreamId { get; set; }
        public string EventType { get; set; }
        public ReadOnlyMemory<byte> Metadata { get; set; }
    }
}
