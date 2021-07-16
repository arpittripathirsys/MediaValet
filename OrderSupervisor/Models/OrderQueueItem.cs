using MediatR;

namespace OrderSupervisor.Models
{
    public class OrderQueueItem : IRequest
    {
        public long OrderId { get; set; }
        public int MagicNumber { get; set; }
        public string OrderText { get; set; }
    }
}
