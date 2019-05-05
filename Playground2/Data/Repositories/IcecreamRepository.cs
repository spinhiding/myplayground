using System.Collections.Generic;
using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Playground2.Data.Repositories
{
    public class IcecreamRepository : IIcecreamRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public IcecreamRepository(ApplicationDbContext context)
        {
            _appDbContext = context;
        }
        public IEnumerable<Icecream> Icecreams => _appDbContext.Icecreams.Include(c => c.Category);

        public Icecream GetIcecreamById(int id) => _appDbContext.Icecreams.FirstOrDefault(p => p.IcecreamId == id);
    }
}
