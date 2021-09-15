using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.API.Requests
{
    public class GetTransferOrderRequest
    {
        public Guid TransferId { get; set; }
    }
}
