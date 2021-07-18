using CoinBazaar.Transfer.Application.Command;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CoinBazaar.Transfer.Application.Validations
{
    public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
    {
        public CreateTransferCommandValidator(ILogger<CreateTransferCommandValidator> logger)
        {
            RuleFor(command => command.FromWallet).NotEmpty();
            RuleFor(command => command.ToWallet).NotEmpty();
            RuleFor(command => command.Amount).NotEmpty();
            RuleFor(command => command.Amount).GreaterThan(0);

            logger.LogTrace("---- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
