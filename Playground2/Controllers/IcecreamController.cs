using Microsoft.AspNetCore.Mvc;
using Playground2.Data.Interfaces;
using Playground2.Data.Models;
using Playground2.Models;
using System.Collections.Generic;

namespace Playground2.Controllers
{
    public class IcecreamController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIcecreamRepository _icecreamRepository;

        public IcecreamController(ICategoryRepository categoryRepository, 
                                    IIcecreamRepository icecreamRepository)
        {
            _categoryRepository = categoryRepository;
            _icecreamRepository = icecreamRepository;
        }

        public ViewResult ListIcecreams(string category)
        {
            
            IcecreamListViewModel model = new IcecreamListViewModel();
            if (category == null)
            {
                model.Icecreams = _icecreamRepository.Icecreams;
            }
            else
            {
                List<Icecream> category_list = new List<Icecream>();
                foreach (Icecream ice in _icecreamRepository.Icecreams)
                {
                    if (ice.Category.Name == category)
                    {
                        category_list.Add(ice);
                    }
                }
                model.Icecreams = category_list;
            }
            model.CurrentCategory = "Nothin";
            
            return View(model);
        }
    }
}
