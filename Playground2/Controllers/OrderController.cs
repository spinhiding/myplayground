using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Playground2.Data.Interfaces;
using Playground2.Data.Models;

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
        
        public ViewResult AddToOrder()
        {
            //Order order = new Order { State = "Order" };
            //var items = _cart.GetCartItems();
            //_cart.CartItems = items;
            //_orderRepository.CreateOrder(order);
            return View();
        }
        public IActionResult PlaceOrder(string state)
        {
            Order order = new Order { State = "Order" };
            var items = _cart.GetCartItems();
            _cart.CartItems = items;
            _orderRepository.CreateOrder(order);

            return RedirectToAction("AddToOrder", order);
        }
        public IActionResult CancelOrder(Order order)
        {
            //Order order = _orderRepository.GetOrderById(OrderId);
            if (order != null)
            {
                _orderRepository.RemoveOrder(order);
            }
            //remove from ordertable
            return RedirectToAction ("Home", "Index");
        }

        public IActionResult PayNow(Order order)
        {
            //TODO: say thank you!
            //ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            //return View();

            if (order != null)
            {
                _orderRepository.RemoveOrder(order);
            }
            //remove from ordertable
            return View("Home", "Index");
        }
    }
}