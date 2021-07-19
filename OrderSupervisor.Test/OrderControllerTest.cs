using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using OrderSupervisor.Commands;
using OrderSupervisor.Controllers;
using OrderSupervisor.Generators.Implementations;
using OrderSupervisor.Generators.Interfaces;
using OrderSupervisor.Models;
using OrderSupervisor.Options;
using OrderSupervisor.RequestHandlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderSupervisor.Test
{
    public class OrderControllerTest
    {
        //private IServiceProvider _serviceProvider;
        private OrderController _orderController;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            // Arrange
            //var builder = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.Test.json");

            //IConfiguration configuration = builder.Build();

            //_serviceProvider = new ServiceCollection()
            //.AddSingleton<IConfiguration>(configuration)
            //.AddOptions()
            //.AddMediatR(typeof(Startup))
            //.AddAutoMapper(typeof(Startup))
            //.AddSingleton<IOrderIdGenerator, OrderIdGenerator>()
            //.AddSingleton<IConfigureOptions<OrderOptions>, OrderConfigureOptions>()
            //.AddSingleton<IConfigureOptions<OrderConfirmationOptions>, OrderConfirmationConfigureOptions>()
            //.BuildServiceProvider();

            _mediator = new Mock<IMediator>();
            //IMediator mediator = _serviceProvider.GetService<IMediator>();

            _orderController = new OrderController(_mediator.Object);
        }

        [Test]
        public async Task CreateOrderTestWithValidRequest()
        {
            //Arrange
            Guid _agentId = Guid.NewGuid();
            long orderId = 10;

            _mediator.Setup(s => s.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken)))
               .ReturnsAsync(new OrderResponse() { OrderId = orderId, AgentId = _agentId });

            // Act
            IActionResult actionResult = await _orderController.CreateOrder(new Models.OrderRequest() { OrderText = "Test Order" });
            OkObjectResult okResult = actionResult as OkObjectResult;
            OrderResponse orderResponse = okResult.Value as OrderResponse;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(orderResponse);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(orderResponse.OrderId == 10);
            Assert.IsTrue(orderResponse.AgentId == _agentId);
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