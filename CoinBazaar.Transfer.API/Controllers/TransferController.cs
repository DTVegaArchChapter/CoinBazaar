using AutoMapper;
using CoinBazaar.Transfer.API.Requests;
using CoinBazaar.Transfer.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoinBazaar.Infrastructure.Models;

namespace CoinBazaar.Transfer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {
        private IMediator _mediator { get; set; }
        private IMapper _mapper { get; set; }
        public TransferController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransferAsync(CreateTransferRequest request)
        {
            DomainCommandResponse domainResponse = await _mediator.Send(_mapper.Map<CreateTransferCommand>(request));

            return Ok(domainResponse);
        }
    }
}
