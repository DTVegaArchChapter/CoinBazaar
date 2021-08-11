using CoinBazaar.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.EventBus
{
    public interface IEventRepository
    {
        public Task<DomainCommandResponse> Publish(DomainEventResult domainEventResult);

        public Task<List<ResolvedEventDTO>> GetAllEvents(Guid aggregateId);
    }
}
