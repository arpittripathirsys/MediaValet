using Azure.Data.Tables;
using MediaValet.Models;
using Microsoft.Extensions.Options;
using OrderAgent.Options;
using OrderAgent.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderAgent.Services.Implementations
{
    public class OrderConfirmationService : IOrderConfirmationService
    {
        private readonly OrderConfirmationOptions _orderConfirmationOptions;
        private TableClient _tableClient;
        
        public OrderConfirmationService(IOptions<OrderConfirmationOptions> orderConfirmationOptions)
        {
            _orderConfirmationOptions = orderConfirmationOptions.Value;
            _tableClient = new TableClient(_orderConfirmationOptions.ConnectionString, _orderConfirmationOptions.TableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task SendAsync(Guid agentId, Order order)
        {
            var confirmation = new OrderConfirmation(agentId, order.OrderId)
            {
                OrderStatus = "Processed"
            };

            await _tableClient.AddEntityAsync(confirmation).ConfigureAwait(false);
        }
    }
}
