using System.Threading.Tasks;

namespace OrderSupervisor.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync();
    }
}
