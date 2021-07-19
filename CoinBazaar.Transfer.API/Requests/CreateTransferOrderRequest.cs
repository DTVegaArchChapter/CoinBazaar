using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.API.Requests
{
    public class CreateTransferRequest
    {
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }
    }
}
