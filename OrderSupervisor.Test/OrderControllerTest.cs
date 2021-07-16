using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using OrderSupervisor.Controllers;
using OrderSupervisor.Generators.Implementations;
using OrderSupervisor.Generators.Interfaces;
using OrderSupervisor.Options;
using System;
using System.Threading.Tasks;

namespace OrderSupervisor.Test
{
    public class OrderControllerTest
    {
        private IServiceProvider _serviceProvider;
        private OrderController _orderController;


        [SetUp]
        public void Setup()
        {
            // Arrange
            var builder = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.Test.json");

            IConfiguration configuration = builder.Build();

            _serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddOptions()
            .AddMediatR(typeof(Startup))
            .AddAutoMapper(typeof(Startup))
            .AddSingleton<IOrderIdGenerator, OrderIdGenerator>()
            .AddSingleton<IConfigureOptions<OrderOptions>, OrderConfigureOptions>()
            .BuildServiceProvider();

            IMediator mediator = _serviceProvider.GetService<IMediator>();
            IMapper mapper = _serviceProvider.GetService<IMapper>();

            _orderController = new OrderController(mediator, mapper);
        }

        [Test]
        public async Task CreateOrderTestWithValidRequest()
        {
            // Act
            IActionResult actionResult = await _orderController.CreateOrder(new Models.OrderRequest() { OrderText = "Test Order" });
            OkObjectResult okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Successfully added order to queue", Convert.ToString(okResult.Value));
        }

        [Test]
        public async Task CreateOrderTestWithInValidRequest()
        {
            // Act
            IActionResult actionResult = await _orderController.CreateOrder(null);
            UnprocessableEntityObjectResult errorResult = actionResult as UnprocessableEntityObjectResult;

            // Assert
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(422, errorResult.StatusCode);
            Assert.AreEqual("Failed to add order to queue", Convert.ToString(errorResult.Value));
        }
   
    }
}