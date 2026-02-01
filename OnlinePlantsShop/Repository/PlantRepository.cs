using OnlinePlantsShop_.Data;
using OnlinePlantsShop_.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlinePlantsShop_.Repositories
{
    public class PlantRepository : IPlant
    {
        private readonly PlantsDbContext _context;

        public PlantRepository(PlantsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Plant> GetAll() => _context.Plants.ToList();

        public Plant? GetById(int id) => _context.Plants.FirstOrDefault(p => p.Id == id);

        public void Add(Plant plant)
        {
            _context.Plants.Add(plant);
        }

        public void Update(Plant plant)
        {
            _context.Plants.Update(plant);
        }

        public void Delete(int id)
        {
            var plant = _context.Plants.Find(id);
            if (plant != null)
            {
                _context.Plants.Remove(plant);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
