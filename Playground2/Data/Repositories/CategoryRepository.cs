using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public CategoryRepository(ApplicationDbContext context)
        {
            _appDbContext = context;
        }
        public IEnumerable<Category> Categories => _appDbContext.Categories;
    }
}
