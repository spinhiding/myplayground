using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground2.Data.Mocks
{
    public class MockIcecreamRepository : IIcecreamRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();
        public IEnumerable<Icecream> Icecreams
        {
            get
            {
                return new List<Icecream>
                {
                    new Icecream {IcecreamId = 1, Cost = 10,
                                  Category = _categoryRepository.Categories.First()},
                    new Icecream {IcecreamId = 2, Cost = 20,
                                  Category = _categoryRepository.Categories.First()}
                };

            }
            set
            {

            }
        }

        public Icecream GetIcecreamById(int id)
        {
            foreach (Icecream icecream in Icecreams)
            {
                if (icecream.IcecreamId == id)
                {
                    return icecream;
                }
            }
            return null;
        }
    }
}
