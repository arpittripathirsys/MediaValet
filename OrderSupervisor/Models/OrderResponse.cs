using MediatR;
using System;

namespace OrderSupervisor.Models
{
    public class OrderResponse
    {
        public long OrderId { get; set; }
        public Guid AgentId { get; set; }
    }
}
