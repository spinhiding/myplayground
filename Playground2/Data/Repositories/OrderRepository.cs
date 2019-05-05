using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly Cart _cart;

        public OrderRepository(ApplicationDbContext context, Cart cart)
        {
            _appDbContext = context;
            _cart = cart;
        }

        public Order GetOrderById(string id) => _appDbContext.Orders.FirstOrDefault(p => p.OrderId == id);

        public void CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            _appDbContext.Orders.Add(order);

            var cartItems = _cart.CartItems;

            foreach(CartItem cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    Amount = cartItem.Amount,
                    Cost = cartItem.Icecream.Cost,
                    OrderId = order.OrderId,
                    IcecreamId = cartItem.Icecream.IcecreamId
                };
                _appDbContext.OrderItems.Add(orderItem);
            }
            _appDbContext.SaveChanges();
        }

        public void RemoveOrder(Order order)
        {
            _cart.ClearCart();
            _appDbContext.Orders.Remove(order);

            _appDbContext.SaveChanges();
        }
    }
}
