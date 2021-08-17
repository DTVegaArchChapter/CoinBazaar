namespace CoinBazaar.Infrastructure.Models
{
    using System;

    public sealed class DomainCommandResponse
    {
        public Guid AggregateId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
