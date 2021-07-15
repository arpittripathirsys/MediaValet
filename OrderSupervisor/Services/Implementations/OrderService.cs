using Azure.Storage.Queues;
using MediaValet.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderSupervisor.Generators.Interfaces;
using OrderSupervisor.Options;
using OrderSupervisor.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderSupervisor.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly OrderOptions _orderOptions;
        private readonly Random _magicNumberGenerator;
        private readonly IOrderIdGenerator _orderIdGenerator;
        private QueueClient _queueClient;

        public OrderService(IOptions<OrderOptions> orderOptions, IOrderIdGenerator orderIdGenerator)
        {
            _orderOptions = orderOptions.Value;
            _orderIdGenerator = orderIdGenerator;
            _magicNumberGenerator = new Random();
            _queueClient = new QueueClient(_orderOptions.ConnectionString, _orderOptions.QueueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task CreateOrderAsync()
        {
            long orderId = _orderIdGenerator.GetNextOrderId();
            int magicNumber = _magicNumberGenerator.Next(1, 10);
            var order = new Order
            {
                OrderId = orderId,
                MagicNumber = magicNumber,
                OrderText = $"Order# {orderId} MagicNumber {magicNumber}"
            };

            await _queueClient.SendMessageAsync(JsonConvert.SerializeObject(order));
            Console.WriteLine($"Send order {orderId} with random number {magicNumber}");
        }
    }
}
