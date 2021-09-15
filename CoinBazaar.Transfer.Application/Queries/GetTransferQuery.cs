using CoinBazaar.Infrastructure.Mongo.Data.Transfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Application.Queries
{
    public class GetTransferQuery : IRequest<TransferModel>
    {
        public Guid TransferId { get; set; }
    }
}
