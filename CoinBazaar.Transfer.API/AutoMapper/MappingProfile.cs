using AutoMapper;
using CoinBazaar.Transfer.API.Requests;
using CoinBazaar.Transfer.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTransferRequest, CreateTransferCommand>();
        }
    }
}
