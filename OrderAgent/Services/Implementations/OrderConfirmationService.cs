using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using OrderAgent.Models;
using OrderAgent.Options;
using OrderAgent.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderAgent.Services.Implementations
{
    public class OrderConfirmationService : IOrderConfirmationService
    {
        private TableClient _tableClient;

        public OrderConfirmationService(IOptions<OrderConfirmationOptions> orderConfirmationOptions)
        {
            _tableClient = new TableClient(orderConfirmationOptions.Value.ConnectionString, orderConfirmationOptions.Value.TableName);
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
