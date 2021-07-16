using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderSupervisor.Models;
using System;
using System.Threading.Tasks;

namespace OrderSupervisor.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                if (orderRequest == null)
                    throw new ArgumentNullException();

                Order order = new Order();
                _mapper.Map(orderRequest, order);
                await _mediator.Send(order);
                return Ok("Successfully added order to queue");
            }
            catch (System.Exception)
            {
                return UnprocessableEntity("Failed to add order to queue");
            }
        }
    }
}
