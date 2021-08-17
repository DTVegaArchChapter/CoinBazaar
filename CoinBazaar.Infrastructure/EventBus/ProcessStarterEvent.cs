namespace CoinBazaar.Infrastructure.EventBus
{
    using System;
    using System.Collections.Generic;

    public class ProcessStarterEvent : IProcessStarterEvent
    {
        public Guid ProcessId { get; set; }

        public string ProcessName { get; set; }

        public IList<KeyValuePair<string, object>> ProcessParameters { get; set; }
    }
}
