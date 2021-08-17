namespace CoinBazaar.Transfer.Application.CommandHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoinBazaar.Infrastructure.EventBus;
    using CoinBazaar.Infrastructure.Models;
    using CoinBazaar.Transfer.Application.Commands;
    using CoinBazaar.Transfer.Domain;

    using MediatR;

    public sealed class TransferCommandHandler : IRequestHandler<CreateTransferCommand, DomainCommandResponse>
    {
        private readonly IEventSourceRepository _eventRepository;

        public TransferCommandHandler(IEventSourceRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<DomainCommandResponse> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            var aggregateRoot = new TransferAggregateRoot(Guid.NewGuid(), request.FromWallet, request.ToWallet, request.Amount);

            await _eventRepository.SaveAsync(aggregateRoot).ConfigureAwait(false);

            return new DomainCommandResponse { AggregateId = aggregateRoot.AggregateId, CreationDate = DateTime.UtcNow };
        }
    }
}
