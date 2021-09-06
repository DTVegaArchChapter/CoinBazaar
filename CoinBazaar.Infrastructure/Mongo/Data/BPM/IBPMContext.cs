using MongoDB.Driver;

namespace CoinBazaar.Infrastructure.Mongo.Data
{
    public interface IBPMContext
    {
        IMongoCollection<Process> Processes { get; }
    }
}
