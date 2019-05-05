using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Playground2.Data.Interfaces;
using Playground2.Data.Repositories;
using Playground2.Models;

namespace Playground2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIcecreamRepository _icecreamRepository;

        public HomeController (IIcecreamRepository repo)
        {
            _icecreamRepository = repo;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Icecreams = _icecreamRepository.Icecreams
            };              

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
