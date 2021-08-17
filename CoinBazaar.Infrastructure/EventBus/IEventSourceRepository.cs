namespace CoinBazaar.Infrastructure.EventBus
{
    using System;
    using System.Threading.Tasks;

    using CoinBazaar.Infrastructure.Aggregates;

    public interface IEventSourceRepository
    {
        Task<TAggregate> FindByIdAsync<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregateRoot, new();

        Task SaveAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot;
    }
}