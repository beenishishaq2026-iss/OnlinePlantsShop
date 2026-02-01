using OnlinePlantsShop_.Models;

namespace OnlinePlantsShop_.Repositories
{
    public interface IPlant
    {
        IEnumerable<Plant> GetAll();
        Plant? GetById(int id);
        void Add(Plant plant);
        void Update(Plant plant);
        void Delete(int id);
        void Save();
    }
}
