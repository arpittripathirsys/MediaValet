using MediaValet.Models;
using System.Threading.Tasks;

namespace OrderSupervisor.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync();
    }
}
