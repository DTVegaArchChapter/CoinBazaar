namespace CoinBazaar.Infrastructure.Aggregates
{
    using System;
    using System.Collections.Generic;

    using CoinBazaar.Infrastructure.EventBus;

    public interface IAggregateRoot
    {
        public Guid AggregateId { get; }

        public long Version { get; }

        public void LoadFromHistory(long version, IEnumerable<IEvent> history);

        public IEnumerable<IEvent> GetUncommittedChanges();

        public void MarkChangesAsCommitted();
    }
}
