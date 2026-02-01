using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlinePlantsShop_.Controllers
{
    public class HomeController : Controller
    {
        
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }
        public ViewResult Privacy()
        {
            return View();
        }
    }
}
//SELECT TOP 100 * FROM dbo.Plants;
//SELECT TOP 100 * FROM dbo.Orders;
//SELECT TOP 100 * FROM dbo.Reviews;
