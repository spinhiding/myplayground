using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using Playground2.Models;

namespace Playground2.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repo, Cart cart)
        {
            _orderRepository = repo;
            _cart = cart;
        }
        
        //public ViewResult AddToOrder()
        //{
            //Order order = new Order { State = "Order" };
            //var items = _cart.GetCartItems();
            //_cart.CartItems = items;
            //_orderRepository.CreateOrder(order);
          //  return View();
        //}
        public ViewResult AddToOrder(string state)
        {
            OrderViewModel model = null;
            if (!(state.Equals("New")))
            {
                Order order = new Order { State = "Order" };
                var items = _cart.GetCartItems();
                _cart.CartItems = items;
                _orderRepository.CreateOrder(order);

                model = new OrderViewModel
                {
                    Order = order,
                    OrderTotal = _cart.GetCartTotal()
                };

            }
            return View (model);
        }
        public IActionResult CancelOrder(string orderId)
        {
            Order order = _orderRepository.GetOrderById(orderId);
            if (order != null)
            {
                _orderRepository.RemoveOrder(order);
            }
            //remove from ordertable
            return RedirectToAction ("Index", "Home");
        }

        public IActionResult PayNow(string orderId)
        {
            //TODO: say thank you!
            //ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            //return View();
            Order order = _orderRepository.GetOrderById(orderId);
            if (order != null)
            {
                _orderRepository.RemoveOrder(order);
            }
            //remove from ordertable
            return RedirectToAction ("Index", "Home");
        }

        public IActionResult ListOrders()
        {

            OrderListViewModel model = new OrderListViewModel();
            model.Orders = _orderRepository.Orders;

            return View(model);
        }
    }
}