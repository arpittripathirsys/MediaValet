using Microsoft.AspNetCore.Mvc;
using OrderSupervisor.Services.Interfaces;
using System.Threading.Tasks;

namespace OrderSupervisor.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                await _orderService.CreateOrderAsync();
                return Ok("Successfully added order to queue");
            }
            catch (System.Exception)
            {
                return UnprocessableEntity("Failed to add order to queue");
            }
        }
    }
}
