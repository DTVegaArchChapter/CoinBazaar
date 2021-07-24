using CoinBazaar.Infrastructure.EventBus;
using CoinBazaar.Transfer.Domain.Events;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Domain
{
    public class TransferAggregateRoot
    {
        #region Properties
        public Guid TransferId { get; set; }
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }
        #endregion

        public TransferAggregateRoot()
        {

        }

        public async Task<DomainEvent> CreateTransfer(string fromWallet, string toWallet, decimal amount)
        {
            var @event = new TransferCreated(fromWallet, toWallet, amount);
            var sampleObject = new { a = 2 };
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sampleObject));
            var metadata = Encoding.UTF8.GetBytes("{}");
            var evt = new EventData(Guid.NewGuid(), "event-type", true, data, metadata);

            return new DomainEvent()
            {
                EventData = evt
            };

            //return new DomainEvent()
            //{
            //    EventData = new EventData(
            //        Guid.NewGuid(),
            //        "TESTEventType",
            //        true,
            //        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event)),
            //        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event.GetType().FullName)))
            //};
        }
    }
}
