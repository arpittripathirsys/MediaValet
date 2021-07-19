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
using OrderSupervisor.Models;
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
            .AddSingleton<IConfigureOptions<OrderConfirmationOptions>, OrderConfirmationConfigureOptions>()
            .BuildServiceProvider();

            IMediator mediator = _serviceProvider.GetService<IMediator>();

            _orderController = new OrderController(mediator);
        }

        [Test]
        public async Task CreateOrderTestWithValidRequest()
        {
            // Act
            IActionResult actionResult = await _orderController.CreateOrder(new Models.OrderRequest() { OrderText = "Test Order" });
            OkObjectResult okResult = actionResult as OkObjectResult;
            OrderConfirmation orderConfirmation = okResult.Value as OrderConfirmation;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(orderConfirmation);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(orderConfirmation.OrderId > 0);
            Assert.IsTrue(orderConfirmation.AgentId != Guid.Empty);
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