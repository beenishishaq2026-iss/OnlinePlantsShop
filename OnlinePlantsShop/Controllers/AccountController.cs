using Microsoft.AspNetCore.Mvc;

namespace OnlinePlantsShop_.Controllers
{
    public class AccountController : Controller
    {

        public ViewResult Register()
        {
            return View();
        }


        public ViewResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
    }
    