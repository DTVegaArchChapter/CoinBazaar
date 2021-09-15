using AutoMapper;
using CoinBazaar.Transfer.API.Requests;
using CoinBazaar.Transfer.Application.Commands;
using CoinBazaar.Transfer.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            var domainResponse = await _mediator.Send(_mapper.Map<CreateTransferCommand>(request));

            return Ok(domainResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransferAsync(Guid id)
        {
            var domainResponse = await _mediator.Send(new GetTransferQuery() { TransferId = id });

            return Ok(domainResponse);
        }
    }
}
