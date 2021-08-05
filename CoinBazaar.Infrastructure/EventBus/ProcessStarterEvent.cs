using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.EventBus
{
    public class ProcessStarterEvent : IProcessStarterEvent
    {
        public Guid ProcessId { get; set; }
        public string ProcessName { get; set; }
        public IList<KeyValuePair<string, object>> ProcessParameters { get; set; }
    }
}
