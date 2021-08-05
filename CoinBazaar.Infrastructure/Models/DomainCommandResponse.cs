using System;

namespace CoinBazaar.Infrastructure.Models
{
    public class DomainCommandResponse
    {
        public Guid AggregateId { get; set; }
        public Guid EventId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
