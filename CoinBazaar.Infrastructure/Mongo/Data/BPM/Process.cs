using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CoinBazaar.Infrastructure.Mongo.Data
{
    public class Process
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public Guid ProcessId { get; set; }
        public string ProcessName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
