using Azure.Storage.Queues;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderSupervisor.Models;
using OrderSupervisor.Options;
using System.Threading;
using System.Threading.Tasks;

namespace OrderSupervisor.RequestHandlers
{
    public class OrderQueueRequestHandler : AsyncRequestHandler<OrderQueueItem>
    {
        private QueueClient _queueClient;

        public OrderQueueRequestHandler(IOptions<OrderOptions> orderOptions)
        {
            _queueClient = new QueueClient(orderOptions.Value.ConnectionString, orderOptions.Value.QueueName);
            _queueClient.CreateIfNotExists();
        }

        protected override async Task Handle(OrderQueueItem orderQueueItem, CancellationToken cancellationToken)
        {
            await _queueClient.SendMessageAsync(JsonConvert.SerializeObject(orderQueueItem));
        }
    }
}
