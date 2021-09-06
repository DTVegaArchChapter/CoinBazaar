using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CoinBazaar.Infrastructure.Mongo.Data.Transfer
{
    public class TransferModel
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public Guid TransferId { get; set; }
        public Guid FromWalletId { get; set; }
        public Guid ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
