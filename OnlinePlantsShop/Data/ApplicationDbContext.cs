using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlinePlantsShop.Models;

namespace OnlinePlantsShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<appuser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
