using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string State { get; set; } //order or pay
        public DateTime OrderDate { get; set; }
    }
}
