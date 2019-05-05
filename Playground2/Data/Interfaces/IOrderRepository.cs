using Playground2.Data.Models;

namespace Playground2.Data.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);

        Order GetOrderById(string id);

        void RemoveOrder(Order order);
    }
}
