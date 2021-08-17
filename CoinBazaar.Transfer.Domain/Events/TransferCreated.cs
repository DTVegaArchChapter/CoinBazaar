using CoinBazaar.Infrastructure.EventBus;
using System;
using System.Collections.Generic;

namespace CoinBazaar.Transfer.Domain.Events
{
    public class TransferCreated : EventBase, IProcessStarterEvent
    {
        public string FromWallet { get; private init; }
        public string ToWallet { get; private init; }
        public decimal Amount { get; private init; }
        public Guid ProcessId { get; set; }
        public string ProcessName { get; set; }
        public IList<KeyValuePair<string, object>> ProcessParameters { get; set; }

        public TransferCreated(Guid id, string fromWallet, string toWallet, decimal amount, Guid processId, IList<KeyValuePair<string, object>> processParameters)
        {
            AggregateId = id;
            FromWallet = fromWallet;
            ToWallet = toWallet;
            Amount = amount;
            ProcessName = "TransferBPMN";
            ProcessId = processId;
            ProcessParameters = processParameters;
        }
    }
}
