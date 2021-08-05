using MongoDB.Driver;

namespace CoinBazaar.Infrastructure.Mongo.Data
{
    public class BPMContext : IBPMContext
    {
        private readonly IMongoDatabase _db;
        public BPMContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Process> Processes => _db.GetCollection<Process>("Process");
    }
}
