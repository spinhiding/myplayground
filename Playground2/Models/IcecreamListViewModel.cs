using Playground2.Data.Models;
using System.Collections.Generic;
namespace Playground2.Models
{
    public class IcecreamListViewModel
    {
        public IEnumerable<Icecream> Icecreams { get; set; }
        public string CurrentCategory { get; set; }
    }
}
