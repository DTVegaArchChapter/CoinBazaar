namespace CoinBazaar.Infrastructure.Mongo
{
    public class MongoServerConfig
    {
        public MongoDBConfig MongoBPMDB { get; set; } = new MongoDBConfig();
        public MongoDBConfig MongoTransferDB { get; set; } = new MongoDBConfig();
    }
}
