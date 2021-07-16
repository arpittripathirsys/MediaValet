using Azure;
using Azure.Data.Tables;
using System;

namespace OrderAgent.Models
{
    public class OrderConfirmation : ITableEntity
    {
        public OrderConfirmation() { }

        public OrderConfirmation(Guid agentId, long orderId)
        {
            PartitionKey = agentId.ToString();
            RowKey = orderId.ToString();
            AgentId = agentId;
            OrderId = orderId;
            Timestamp = DateTime.UtcNow;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public Guid AgentId { get; set; }
        public long OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}
