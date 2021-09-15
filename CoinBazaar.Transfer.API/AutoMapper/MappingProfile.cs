using AutoMapper;
using CoinBazaar.Transfer.API.Requests;
using CoinBazaar.Transfer.Application.Commands;
using CoinBazaar.Transfer.Application.Queries;

namespace CoinBazaar.Transfer.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTransferRequest, CreateTransferCommand>();
            CreateMap<GetTransferOrderRequest, GetTransferQuery>();
        }
    }
}
