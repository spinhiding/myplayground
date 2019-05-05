using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category { CategoryId = 1, Name = "Icecream"},
                    new Category { CategoryId = 2, Name = "Drink"}
                };
            }
        }
    }
}
