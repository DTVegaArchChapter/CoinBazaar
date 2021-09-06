using MongoDB.Driver;

namespace CoinBazaar.Infrastructure.Mongo.Data.Transfer
{
    public class TransferStateModelContext
    {
        private readonly IMongoDatabase _db;
        public TransferStateModelContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<TransferModel> Transfers => _db.GetCollection<TransferModel>("Transfer");
    }
}
