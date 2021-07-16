using OrderAgent.Models;
using System;
using System.Threading.Tasks;

namespace OrderAgent.Services.Interfaces
{
    public interface IOrderConfirmationService
    {
        Task SendAsync(Guid agentId, Order order);
    }
}
