using Microsoft.AspNetCore.Identity;
namespace OnlinePlantsShop.Models;

public class appuser : IdentityUser
{
    public string City { get; set; }
    public string Country { get; set; }
}
