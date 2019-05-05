using Microsoft.AspNetCore.Mvc;
using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using Playground2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Controllers
{
    public class CartController : Controller
    {
        private readonly IIcecreamRepository _icecreamRepository;
        private readonly Cart _cart;
        public CartController(IIcecreamRepository repo, Cart cart)
        {
            _icecreamRepository = repo;
            _cart = cart;
        }

        public ViewResult Index()
        {
            var items = _cart.GetCartItems();
            _cart.CartItems = items;

            CartViewModel model = new CartViewModel
            {
                Cart = _cart,
                CartTotal = _cart.GetCartTotal()
            };
            return View(model);
        }

        public RedirectToActionResult AddToCart(int icecreamid)
        {
            Icecream icecream = _icecreamRepository.GetIcecreamById(icecreamid);
            if (icecream != null)
            {
                _cart.AddToCart(icecream, 1);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromCart(int icecreamid)
        {
            Icecream icecream = _icecreamRepository.GetIcecreamById(icecreamid);
            if (icecream != null)
            {
                _cart.RemoveFromCart(icecream);
            }
            return RedirectToAction("Index");
        }
    }
}
