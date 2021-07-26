using CoinBazaar.Infrastructure.Models;
using System.Threading.Tasks;

namespace CoinBazaar.Infrastructure.EventBus
{
    public interface IEventRepository
    {
        public string StreamName { get; set; }
        public Task<DomainCommandResponse> Publish(DomainEventResult domainEventResult);
    }
}
