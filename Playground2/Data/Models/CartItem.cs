using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Icecream Icecream { get; set; }
        public int Amount { get; set; }
        public string CartId { get; set; }
    }
}
