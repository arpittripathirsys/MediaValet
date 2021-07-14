using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace OrderAgent.Options
{
    public class OrderConfigureOptions : IConfigureOptions<OrderOptions>
    {
        private readonly IConfigurationRoot _configuration;
        public OrderConfigureOptions(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }
        public void Configure(OrderOptions options)
        {
            var configSection = _configuration.GetSection("Orders");
            options.ConnectionString = configSection.GetValue<string>("ConnectionString");
            options.QueueName = configSection.GetValue<string>("QueueName");
        }
    }
}
