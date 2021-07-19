using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Domain
{
    public class TransferAggregateRoot
    {
        #region Properties
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }
        #endregion

        public TransferAggregateRoot()
        {

        }
    }
}
