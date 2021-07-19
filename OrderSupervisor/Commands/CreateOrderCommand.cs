using MediatR;
using OrderSupervisor.Models;

namespace OrderSupervisor.Commands
{
    public class CreateOrderCommand : IRequest<OrderResponse>
    {
        public CreateOrderCommand(OrderRequest orderRequest)
        {
            OrderRequest = orderRequest;
        }
        public OrderRequest OrderRequest { get; set; }
    }
}
