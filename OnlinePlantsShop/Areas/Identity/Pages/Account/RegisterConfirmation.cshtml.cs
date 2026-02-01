using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlinePlantsShop.Areas.Identity.Pages.Account
{
    public class RegisterConfirmationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }  

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }  

        public void OnGet(string email = null, string returnUrl = null)
        {
            Email = email;
            ReturnUrl = returnUrl;
        }
    }
}
