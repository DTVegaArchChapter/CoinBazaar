using CoinBazaar.Infrastructure.Models;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.EventBus
{
    public interface IEventRepository
    {
        public Task<DomainCommandResponse> Publish(DomainEventResult domainEventResult);
    }
}
