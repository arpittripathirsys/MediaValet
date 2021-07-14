using MediaValet.Models;
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
        public async Task<Order> CreateOrder()
        {
            return await _orderService.CreateOrderAsync();
        }
    }
}
