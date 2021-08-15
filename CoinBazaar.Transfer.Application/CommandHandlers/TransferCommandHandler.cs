using CoinBazaar.Infrastructure.EventBus;
using CoinBazaar.Infrastructure.Models;
using CoinBazaar.Transfer.Application.Commands;
using CoinBazaar.Transfer.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoinBazaar.Transfer.Application.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, DomainCommandResponse>
    {
        private readonly IEventRepository _eventRepository;
        public TransferCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<DomainCommandResponse> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            var aggregateRoot = new TransferAggregateRoot(Guid.NewGuid());

            var domainEventResult = aggregateRoot.CreateTransfer(request.FromWallet, request.ToWallet, request.Amount);

            return await _eventRepository.Publish(domainEventResult);
        }
    }
}
