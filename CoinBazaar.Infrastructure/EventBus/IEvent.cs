using System;

namespace CoinBazaar.Infrastructure.EventBus
{
    public interface IEvent
    {
        public Guid Id { get; set; }
    }
}
