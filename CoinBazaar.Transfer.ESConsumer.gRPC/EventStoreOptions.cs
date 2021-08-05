using EventStore.Client;

namespace CoinBazaar.Transfer.ESConsumer.gRPC
{
    public class EventStoreOptions : EventStoreClientSettings
    {
        public string ConnectionString { get; set; }
        public string AggregateStream { get; set; }
        public string PersistentSubscriptionGroup { get; set; }
    }
}
