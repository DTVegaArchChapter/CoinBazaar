using CoinBazaar.Infrastructure.EventBus;
using CoinBazaar.Transfer.Application.Commands;
using CoinBazaar.Transfer.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Application.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventRepository _eventRepository;
        public TransferCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            var aggregateRoot = new TransferAggregateRoot();

            var domainEvent = await aggregateRoot.CreateTransfer(request.FromWallet, request.ToWallet, request.Amount);

            return await _eventRepository.Publish(domainEvent.EventData);

            //return domainCommandResponse??
        }
    }
}
