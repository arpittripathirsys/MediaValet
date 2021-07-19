using Azure;
using Azure.Data.Tables;
using System;

namespace OrderSupervisor.Models
{
    public class OrderConfirmation : ITableEntity
    {
        public OrderConfirmation() { }

        public OrderConfirmation(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
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
