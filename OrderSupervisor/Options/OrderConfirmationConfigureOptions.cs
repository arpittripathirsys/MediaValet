using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace OrderSupervisor.Options
{
    public class OrderConfirmationConfigureOptions : IConfigureOptions<OrderConfirmationOptions>
    {
        private readonly IConfiguration _configuration;
        public OrderConfirmationConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(OrderConfirmationOptions options)
        {
            var configSection = _configuration.GetSection("Confirmations");
            options.ConnectionString = configSection.GetValue<string>("ConnectionString");
            options.TableName = configSection.GetValue<string>("TableName");
        }
    }
}
