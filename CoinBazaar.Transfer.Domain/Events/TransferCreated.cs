using CoinBazaar.Infrastructure.EventBus;

namespace CoinBazaar.Transfer.Domain.Events
{
    public class TransferCreated : EventBase
    {
        public string FromWallet { get; private init; }
        public string ToWallet { get; private init; }
        public decimal Amount { get; private init; }

        public TransferCreated(string fromWallet, string toWallet, decimal amount)
        {
            FromWallet = fromWallet;
            ToWallet = toWallet;
            Amount = amount;
        }
    }
}
