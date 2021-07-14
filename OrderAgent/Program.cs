using Microsoft.Extensions.DependencyInjection;
using OrderAgent.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderAgent
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            IOrderProcessorService orderProcessorService = serviceProvider.GetService<IOrderProcessorService>();
            await orderProcessorService.ProcessOrdersAsync();
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
