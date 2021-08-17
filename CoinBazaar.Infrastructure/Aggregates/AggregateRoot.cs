namespace CoinBazaar.Infrastructure.Aggregates
{
    using System;
    using System.Collections.Generic;

    using CoinBazaar.Infrastructure.EventBus;

    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly ICollection<IEvent> _uncommittedEvents = new List<IEvent>();

        public Guid AggregateId { get; protected set; } = Guid.Empty;

        public long Version { get; private set; } = -1;

        public void MarkChangesAsCommitted()
        {
            _uncommittedEvents.Clear();
        }

        protected abstract void When(IEvent @event);

        public void RaiseEvent(IEvent @event)
        {
            When(@event);
            _uncommittedEvents.Add(@event);
        }

        public void LoadFromHistory(long version, IEnumerable<IEvent> history)
        {
            Version = version;

            foreach (var @event in history)
            {
                When(@event);
            }
        }

        public IEnumerable<IEvent> GetUncommittedChanges() => _uncommittedEvents;
    }
}