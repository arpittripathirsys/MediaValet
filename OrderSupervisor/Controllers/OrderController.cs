using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderSupervisor.Commands;
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
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                if (orderRequest == null)
                    throw new ArgumentNullException();

                CreateOrderCommand createOrderCommand = new CreateOrderCommand(orderRequest);
                OrderResponse orderResponse = await _mediator.Send(createOrderCommand);

                return Ok(orderResponse);
            }
            catch (System.Exception)
            {
                return UnprocessableEntity("Failed to add order to queue");
            }
        }
    }
}
