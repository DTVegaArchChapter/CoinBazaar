using AutoMapper;
using CoinBazaar.Infrastructure.Mongo.Data.Transfer;
using CoinBazaar.Transfer.Domain;

namespace CoinBazaar.Transfer.ESConsumer.gRPC.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransferAggregateRoot, TransferModel>();
        }
    }
}
