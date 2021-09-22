using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class TransferRequest
    {
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public double Amount { get; set; }
    }
}
