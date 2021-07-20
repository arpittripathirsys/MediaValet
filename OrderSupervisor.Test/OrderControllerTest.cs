using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OrderSupervisor.Commands;
using OrderSupervisor.Controllers;
using OrderSupervisor.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderSupervisor.Test
{
    public class OrderControllerTest
    {
        private OrderController _orderController;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _orderController = new OrderController(_mediator.Object);
        }

        [Test]
        public async Task CreateOrderTestWithValidRequest()
        {
            //Arrange
            OrderResponse orderResponse = new OrderResponse()
            {
                OrderId = 10,
                AgentId = Guid.NewGuid()
            };

            _mediator.Setup(s => s.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken)))
               .ReturnsAsync(orderResponse);

            // Act
            IActionResult actionResult = await _orderController.CreateOrder(new OrderRequest() { OrderText = "Test Order" });
            OkObjectResult okResult = actionResult as OkObjectResult;
            OrderResponse actualOrderResponse = okResult.Value as OrderResponse;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(actualOrderResponse);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(orderResponse.OrderId == actualOrderResponse.OrderId);
            Assert.IsTrue(orderResponse.AgentId == actualOrderResponse.AgentId);
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
            Assert.AreEqual("Invalid Request", Convert.ToString(errorResult.Value));
        }

        [Test]
        public async Task CreateOrderTestQueueError()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken)))
            .Throws(new RequestFailedException("Request Failed"));

            // Act
            IActionResult actionResult = await _orderController.CreateOrder(new OrderRequest() { OrderText = "Test Order" });
            UnprocessableEntityObjectResult errorResult = actionResult as UnprocessableEntityObjectResult;

            // Assert
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(422, errorResult.StatusCode);
            Assert.AreEqual("Failed to add order to queue", Convert.ToString(errorResult.Value));
        }

        [Test]
        public async Task CreateOrderTestGenericError()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken)))
            .Throws(new Exception("Request Failed"));

            // Act
            IActionResult actionResult = await _orderController.CreateOrder(new OrderRequest() { OrderText = "Test Order" });
            UnprocessableEntityObjectResult errorResult = actionResult as UnprocessableEntityObjectResult;

            // Assert
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(422, errorResult.StatusCode);
            Assert.AreEqual("Failed to create order", Convert.ToString(errorResult.Value));
        }

    }
}