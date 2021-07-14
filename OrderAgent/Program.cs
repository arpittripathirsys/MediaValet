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

            try
            {
                await orderProcessorService.ProcessOrdersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex}");
            }


            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
