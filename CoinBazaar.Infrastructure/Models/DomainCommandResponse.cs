using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.Models
{
    public class DomainCommandResponse
    {
        public Guid AggregateId { get; set; }
        public Guid EventId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
