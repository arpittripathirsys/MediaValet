using MediaValet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderSupervisor.Services.Interfaces;
using System.Threading.Tasks;

namespace OrderSupervisor.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger,IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<Order> CreateOrder()
        {
            return await _orderService.CreateOrderAsync();
        }
    }
}
