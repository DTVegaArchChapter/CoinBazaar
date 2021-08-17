namespace CoinBazaar.Transfer.Domain
{
    using System;
    using System.Collections.Generic;

    using CoinBazaar.Infrastructure.Aggregates;
    using CoinBazaar.Infrastructure.Annotations;
    using CoinBazaar.Infrastructure.EventBus;
    using CoinBazaar.Transfer.Domain.Events;

    [StreamName("Transfer")]
    public class TransferAggregateRoot : AggregateRoot
    {
        public Guid TransferId { get; set; }
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }

        public TransferAggregateRoot()
        {
        }

        public TransferAggregateRoot(Guid transferId, string fromWallet, string toWallet, decimal amount)
        {
            AggregateId = transferId;
            TransferId = transferId;
            FromWallet = fromWallet;
            ToWallet = toWallet;
            Amount = amount;

            var processParameters = new List<KeyValuePair<string, object>>
                                        {
                                            new("TransferId", TransferId),
                                            new("FromWallet", fromWallet),
                                            new("ToWallet", toWallet),
                                            new("Amount", amount)
                                        };

            RaiseEvent(new TransferCreated(transferId, fromWallet, toWallet, amount, Guid.NewGuid(), processParameters));
        }

        protected override void When(IEvent @event)
        {
            switch (@event)
            { 
                case TransferCreated e:
                    OnTransferCreated(e);
                    break;
            }
        }

        private void OnTransferCreated(TransferCreated e)
        {
            AggregateId = e.AggregateId;
            TransferId = e.AggregateId;
            FromWallet = e.FromWallet;
            ToWallet = e.ToWallet;
            Amount = e.Amount;
        }
    }
}
