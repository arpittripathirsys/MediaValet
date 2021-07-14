using MediaValet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderAgent.Services.Interfaces
{
    public interface IOrderConfirmationService
    {
        Task SendAsync(Guid agentId,Order order);
    }
}
