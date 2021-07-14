using OrderSupervisor.Generators.Interfaces;
using System.IO;

namespace OrderSupervisor.Generators.Implementations
{
    public class OrderIdGenerator : IOrderIdGenerator
    {
        const string OrderFileName = "OrderId.txt";
        private long _orderId = 0;
        private static volatile object _lockObject = new object();
        public OrderIdGenerator()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (File.Exists(OrderFileName))
            {
                string fileData = File.ReadAllText(OrderFileName);
                long.TryParse(fileData, out _orderId);
            }
        }

        public long GetNextOrderId()
        {
            lock (_lockObject)
            {
                _orderId += 1;
                File.WriteAllText(OrderFileName, _orderId.ToString());
                return _orderId;
            }
        }
    }
}
