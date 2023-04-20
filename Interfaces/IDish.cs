using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IDish
    {
        public Task<List<Dish>> GetAll();
        public Task<Dish> GetById(string id);
        public Task<bool> CreateDish(Dish Dish);
        public Task<bool> DeteleDish(Dish Dish);
        public Task<bool> UpdateDish(Dish Dish);
    }
}
