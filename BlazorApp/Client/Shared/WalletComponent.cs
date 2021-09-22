using BlazorApp.Shared;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Shared
{
    public partial class WalletComponet
    {

        private string FromWallet { get; set; }
        private string ToWallet { get; set; }
        private double Amount { get; set; }


        private async Task SendItem()
        {
            try
            {
                var addItem = new TransferRequest { FromWallet = FromWallet, ToWallet = ToWallet, Amount = Amount };
                var response = await Http.PostAsJsonAsync<TransferRequest>("https://localhost:44311/Transfer", addItem);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task Test()
        {

        }
    }
}
