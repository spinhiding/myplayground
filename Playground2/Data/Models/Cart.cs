using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Models
{
    public class Cart
    {
        private readonly ApplicationDbContext _appDbContext;
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }

        private Cart(ApplicationDbContext context)
        {
            _appDbContext = context;
        }


        public static Cart GetCart(IServiceProvider services)
        {
            /*
            using (var scope = services.CreateScope())
            {
                ISession session = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

                session.SetString("CartId", cartId);

                return new Cart(context) { CartId = cartId };
            }
            */
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new Cart(context) { CartId = cartId };
        }

        public void AddToCart(Icecream ice, int amount)
        {
            var cartItem =
                    _appDbContext.CartItems.SingleOrDefault(
                        s => s.Icecream.IcecreamId == ice.IcecreamId && s.CartId == CartId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Icecream = ice,
                    Amount = 1,
                    CartId = CartId
                };

                _appDbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }
        public int RemoveFromCart(Icecream ice)
        {
            var remainingQty = 0;
            var cartItem =
                    _appDbContext.CartItems.SingleOrDefault(
                        s => s.Icecream.IcecreamId == ice.IcecreamId && s.CartId == CartId);

            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                    remainingQty = cartItem.Amount;
                }
                else
                {
                    _appDbContext.CartItems.Remove(cartItem);
                }
            }
            _appDbContext.SaveChanges();
            return remainingQty;
        }

        public List<CartItem> GetCartItems()
        {
            return CartItems ??
                   (CartItems = _appDbContext.CartItems.Where(c => c.CartId == CartId)
                           .Include(s => s.Icecream).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext.CartItems.Where(cart => cart.CartId == CartId);

            _appDbContext.CartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetCartTotal()
        {
            var total = _appDbContext.CartItems.Where(c => c.CartId == CartId)
                .Select(c => c.Icecream.Cost * c.Amount).Sum();
            return total;
        }
    }
}
