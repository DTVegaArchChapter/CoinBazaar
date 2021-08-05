namespace CoinBazaar.Transfer.API.Requests
{
    public class CreateTransferRequest
    {
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }
    }
}
