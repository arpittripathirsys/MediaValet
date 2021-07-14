using System.Threading.Tasks;

namespace OrderAgent.Services.Interfaces
{
    public interface IOrderProcessorService
    {
        Task ProcessOrdersAsync();
    }
}
