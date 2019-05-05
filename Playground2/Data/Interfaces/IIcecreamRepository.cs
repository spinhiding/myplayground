using Playground2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Interfaces
{
    public interface IIcecreamRepository
    {
        IEnumerable<Icecream> Icecreams { get; }
        Icecream GetIcecreamById(int id);

    }
}
