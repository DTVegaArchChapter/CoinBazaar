using CoinBazaar.Transfer.Application.Command;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Application.CommandHandler
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
