using Microsoft.Extensions.Options;

namespace OrderSupervisor.Options
{
   public class OrderConfirmationOptions : IOptions<OrderConfirmationOptions>
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public OrderConfirmationOptions Value => this;
    }
}
