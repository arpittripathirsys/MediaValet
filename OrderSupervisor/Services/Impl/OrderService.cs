using Azure.Storage.Queues;
using MediaValet.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderSupervisor.Options;
using OrderSupervisor.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderSupervisor.Services.Impl
{
    public class OrderService : IOrderService
    {
        private long orderIdSequence = 0;
        private readonly OrderOptions _orderOptions;
        private QueueClient _queueClient;
        private readonly Random _magicNumberGenerator;
        public OrderService(IOptions<OrderOptions> orderOptions)
        {
            _orderOptions = orderOptions.Value;
            CreateIfNotExistAsync();
            _magicNumberGenerator = new Random(1);
        }

        public async Task<Order> CreateOrderAsync()
        {
            long orderId = Interlocked.Increment(ref orderIdSequence);
            long magicNumber = _magicNumberGenerator.Next(1, 10);
            var order = new Order
            {
                OrderId = orderId,
                MagicNumber = magicNumber,
                OrderText = $"Order# {orderId} MagicNumber {magicNumber}"
            };

            await _queueClient.SendMessageAsync(JsonConvert.SerializeObject(order));
            Console.WriteLine($"Send order {orderId} with random number {magicNumber}");
            return order;
        }

        private void CreateIfNotExistAsync()
        {
            _queueClient = new QueueClient(_orderOptions.ConnectionString, _orderOptions.QueueName);
            _queueClient.CreateIfNotExists();
        }
    }
}
