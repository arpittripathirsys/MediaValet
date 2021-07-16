using MediatR;

namespace OrderSupervisor.Models
{
    public class Order : IRequest
    {
        public long OrderId { get; set; }
        public int MagicNumber { get; set; }
        public string OrderText { get; set; }
    }
}
