using MediatR;
using OrderSupervisor.Models;

namespace OrderSupervisor.Commands
{
    public class GetOrderConfirmationCommand : IRequest<OrderConfirmation>
    {
        public GetOrderConfirmationCommand(long orderId)
        {
            OrderId = orderId;
        }
        public long OrderId { get; set; }
    }
}
