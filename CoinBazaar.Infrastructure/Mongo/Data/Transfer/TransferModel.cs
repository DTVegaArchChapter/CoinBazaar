using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CoinBazaar.Infrastructure.Mongo.Data.Transfer
{
    [BsonIgnoreExtraElements]
    public class TransferModel
    {
        public string TransferId { get; set; }
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }
    }
}
