using Playground2.Data.Models;
using System.Collections.Generic;

namespace Playground2.Data.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        void CreateOrder(Order order);

        Order GetOrderById(string id);

        void RemoveOrder(Order order);
    }
}
