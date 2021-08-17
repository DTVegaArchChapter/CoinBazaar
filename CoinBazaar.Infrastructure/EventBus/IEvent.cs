namespace CoinBazaar.Infrastructure.EventBus
{
    using System;

    public interface IEvent
    {
        Guid Id { get; set; }

        Guid AggregateId { get; }

        long Version { get; set; }
    }
}
