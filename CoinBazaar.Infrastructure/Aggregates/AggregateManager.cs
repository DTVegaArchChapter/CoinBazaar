using CoinBazaar.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.Aggregates
{
    public class AggregateManager<T> where T : IAggregateRoot
    {
        public T Instance(Guid aggregateId)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { aggregateId });
        }
        public T ApplyAll(Guid aggregateId, List<ResolvedEventDTO> events)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { aggregateId, events });
        }
    }
}
