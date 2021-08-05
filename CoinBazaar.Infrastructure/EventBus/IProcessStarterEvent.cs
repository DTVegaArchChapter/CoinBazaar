using System;
using System.Collections.Generic;

namespace CoinBazaar.Infrastructure.EventBus
{
    public interface IProcessStarterEvent
    {
        public Guid ProcessId { get; set; }
        public string ProcessName { get; set; }
        public IList<KeyValuePair<string, object>> ProcessParameters { get; set; }
    }
}
