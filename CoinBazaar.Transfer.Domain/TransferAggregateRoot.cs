using CoinBazaar.Infrastructure.Aggregates;
using CoinBazaar.Infrastructure.Annotations;
using CoinBazaar.Infrastructure.EventBus;
using CoinBazaar.Infrastructure.Helpers;
using CoinBazaar.Transfer.Domain.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Domain
{
    [StreamName("Transfer")]
    public class TransferAggregateRoot : IAggregateRoot
    {
        #region Properties
        public Guid TransferId { get; set; }
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }
        #endregion

        public TransferAggregateRoot(Guid transferId)
        {
            TransferId = transferId;
        }

        public async Task<DomainEventResult> CreateTransfer(string fromWallet, string toWallet, decimal amount)
        {
            var processParameters = new List<KeyValuePair<string, object>>();
            processParameters.Add(new KeyValuePair<string, object>("TransferId", TransferId));
            processParameters.Add(new KeyValuePair<string, object>("FromWallet", fromWallet));
            processParameters.Add(new KeyValuePair<string, object>("ToWallet", toWallet));
            processParameters.Add(new KeyValuePair<string, object>("Amount", amount));

            var @event = new TransferCreated(fromWallet, toWallet, amount, Guid.NewGuid(), processParameters);

            return await DomainResponseHelper.CreateDomainResponse(TransferId, true, @event, null);
        }
    }
}
