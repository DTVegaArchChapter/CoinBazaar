using CoinBazaar.Infrastructure.Mongo.Data.Transfer;
using CoinBazaar.Transfer.Application.Queries;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Application.QueryHandlers
{
    public sealed class TransferQueryHandler : IRequestHandler<GetTransferQuery, TransferModel>
    {
        private readonly TransferStateModelContext _transferStateModelContext;
        public TransferQueryHandler(TransferStateModelContext transferStateModelContext)
        {
            _transferStateModelContext = transferStateModelContext;
        }
        public async Task<TransferModel> Handle(GetTransferQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<TransferModel>.Filter.Eq(x => x.TransferId, request.TransferId.ToString());

            var transfer = (await _transferStateModelContext.Transfers.FindAsync(filter)).FirstOrDefault();

            return transfer;
        }
    }
}
