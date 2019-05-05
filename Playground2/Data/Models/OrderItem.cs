using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string OrderId { get; set; }
        public int IcecreamId { get; set; }
        public int Amount { get; set; }
        public int Cost { get; set; }
        //public virtual Icecream Icecream { get; set; }
        //public virtual Order Order { get; set; }
    }
}
