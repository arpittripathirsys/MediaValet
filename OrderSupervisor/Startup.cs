using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OrderSupervisor.Generators.Implementations;
using OrderSupervisor.Generators.Interfaces;
using OrderSupervisor.Options;
using OrderSupervisor.Services.Implementations;
using OrderSupervisor.Services.Interfaces;

namespace OrderSupervisor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();
            services.AddSingleton<IOrderIdGenerator, OrderIdGenerator>();
            services.AddSingleton<IConfigureOptions<OrderOptions>, OrderConfigureOptions>();
            services.AddSingleton<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
