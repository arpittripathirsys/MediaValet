using Microsoft.Extensions.Options;

namespace OrderSupervisor.Options
{
    public class OrderOptions : IOptions<OrderOptions>
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
        public OrderOptions Value => this;
    }
}
