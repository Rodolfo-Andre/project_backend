using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface IDish
    {
        Task<List<Dish>> GetDishs();
        Task<Dish> GetDish(string id);

        Task<bool> CreateDish(Dish Dish);

        Task<bool> DeteleDish(Dish Dish);

        Task<bool> UpdateDish(Dish Dish);


    }
}
