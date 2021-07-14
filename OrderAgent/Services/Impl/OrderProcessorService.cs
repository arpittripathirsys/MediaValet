using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using MediaValet.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderAgent.Options;
using OrderAgent.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderAgent.Services.Impl
{
    public class OrderProcessorService : IOrderProcessorService
    {
        private readonly IOrderConfirmationService _orderConfirmationService;
        private readonly OrderOptions _orderOptions;
        private QueueClient _queueClient;
        private Guid _agentId;
        private long _magicNumber;
        public OrderProcessorService(IOptions<OrderOptions> orderOptions, IOrderConfirmationService orderConfirmationService)
        {
            _orderOptions = orderOptions.Value;
            _orderConfirmationService = orderConfirmationService;
        }

        public async Task ProcessOrdersAsync()
        {
            await InitializeAgent();

            bool doProcess = true;
            while (doProcess)
            {
                var message = await _queueClient.ReceiveMessageAsync();
                if (message != null && message.Value != null)
                    doProcess = await ProcessOrderAsync(message.Value);
            }
        }

        private async Task InitializeAgent()
        {
            await CreateIfNotExistAsync();
            _agentId = Guid.NewGuid();
            Random random = new Random(1);
            _magicNumber = random.Next(1, 10);
            Console.WriteLine($"I’m agent {_agentId}, my magic number is {_magicNumber}");
        }

        private async Task<bool> ProcessOrderAsync(QueueMessage queueMessage)
        {
            bool canProcessNextMessage = true;
            var order = JsonConvert.DeserializeObject<Order>(queueMessage.MessageText);
            Console.WriteLine($"Received order {order.OrderId}");

            if (order.MagicNumber == _magicNumber)
            {
                canProcessNextMessage = false;
                Console.WriteLine("Oh no, my magic number was found");
            }
            else
            {
                Console.WriteLine($"Order Text: {order.OrderText}");
                await _orderConfirmationService.SendAsync(_agentId, order);
                await _queueClient.DeleteMessageAsync(queueMessage.MessageId, queueMessage.PopReceipt);
            }

            return canProcessNextMessage;
        }

        private async Task CreateIfNotExistAsync()
        {
            _queueClient = new QueueClient(_orderOptions.ConnectionString, _orderOptions.QueueName);
            await _queueClient.CreateIfNotExistsAsync();
        }
    }
}
