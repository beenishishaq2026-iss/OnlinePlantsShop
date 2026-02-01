using Microsoft.EntityFrameworkCore;
using OnlinePlantsShop_.Models;

namespace OnlinePlantsShop_.Data
{
    public class PlantsDbContext : DbContext
    {
        public PlantsDbContext(DbContextOptions<PlantsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plants { get; set; }
        public object Reviews { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.ImageUrl).IsRequired();
                entity.Property(p => p.Description).IsRequired();
                entity.Property(p => p.Type).IsRequired();
                entity.Property(p => p.Price).IsRequired();
            });



        }
    }
}

