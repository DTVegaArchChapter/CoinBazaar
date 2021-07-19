using MediatR;

namespace CoinBazaar.Transfer.Application.Commands
{
    public class CreateTransferCommand : IRequest<bool>
    {
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public decimal Amount { get; set; }
    }
}
