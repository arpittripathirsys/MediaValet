using Azure.Data.Tables;
using MediatR;
using Microsoft.Extensions.Options;
using OrderSupervisor.Commands;
using OrderSupervisor.Models;
using OrderSupervisor.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderSupervisor.RequestHandlers
{
    public class OrderConfirmationHandler : IRequestHandler<GetOrderConfirmationCommand, OrderConfirmation>
    {

        private TableClient _tableClient;
        public OrderConfirmationHandler(IOptions<OrderConfirmationOptions> orderConfirmationOptions)
        {
            _tableClient = new TableClient(orderConfirmationOptions.Value.ConnectionString, orderConfirmationOptions.Value.TableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task<OrderConfirmation> Handle(GetOrderConfirmationCommand request, CancellationToken cancellationToken)
        {
            return await QueryOrderConfirmationAsync(request.OrderId);
        }


        private async Task<OrderConfirmation> QueryOrderConfirmationAsync(long orderId)
        {
            OrderConfirmation orderConfirmation = null;
            var queryResult = _tableClient.QueryAsync<OrderConfirmation>(s => s.OrderId == orderId);

            await foreach (var queryResultItem in queryResult)
            {
                if (queryResultItem != null)
                    orderConfirmation = queryResultItem;
            }

            //This will go in infinite loop. To break this we need to implement number of retries. 
            if (orderConfirmation == null)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                orderConfirmation = await QueryOrderConfirmationAsync(orderId);
            }

            return orderConfirmation;
        }

    }
}
