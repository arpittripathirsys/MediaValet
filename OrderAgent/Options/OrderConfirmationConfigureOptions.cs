using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace OrderAgent.Options
{
    public class OrderConfirmationConfigureOptions : IConfigureOptions<OrderConfirmationOptions>
    {
        private readonly IConfigurationRoot _configuration;
        public OrderConfirmationConfigureOptions(IConfigurationRoot configuration)
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
