using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Models
{
    public class Icecream
    {
        public int IcecreamId { set; get; }
        public int Cost { set; get; }
        public int CategoryId { set; get; }

        public virtual Category Category { get; set; }
    }
}
