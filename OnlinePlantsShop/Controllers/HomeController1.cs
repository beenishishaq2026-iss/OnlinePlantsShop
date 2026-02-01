using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlinePlantsShop.Models;
using OnlinePlantsShop_.Models;

namespace OnlinePlantsShop_.Controllers
{
    public class HomeController1 : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController1(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
