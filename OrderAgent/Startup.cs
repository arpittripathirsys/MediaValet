using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderAgent.Options;
using OrderAgent.Services.Implementations;
using OrderAgent.Services.Interfaces;

namespace OrderAgent
{
    public class Startup
    {
        IConfiguration configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddOptions();
            services.AddSingleton<IConfigureOptions<OrderOptions>, OrderConfigureOptions>();
            services.AddSingleton<IConfigureOptions<OrderConfirmationOptions>, OrderConfirmationConfigureOptions>();
            services.AddSingleton<IOrderConfirmationService, OrderConfirmationService>();
            services.AddSingleton<IOrderProcessorService, OrderProcessorService>();


        }
    }
}
