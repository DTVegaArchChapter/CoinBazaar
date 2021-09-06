using MongoDB.Driver;

namespace CoinBazaar.Infrastructure.Mongo.Data
{
    public class BPMContext : IBPMContext
    {
        private readonly IMongoDatabase _db;
        public BPMContext(MongoDBConfig config)
        {
            //mongodb://root:*****@localhost:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false
            //var client = new MongoClient("mongodb://root:example@mongodb:27017/BPMDB?authSource=admin&ssl=false");
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Process> Processes => _db.GetCollection<Process>("Process");
    }
}
