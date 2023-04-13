using project_backend.Models;

using project_backend.Schemas.project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface ICategoryDish
    {
        Task<List<CategoryDish>> GetCategoryDishs();

        Task<CategoryDish> GetCategoryDish(string id);

        Task<bool> CreateCategoryDish(CategoryDish categoryDish);

        Task<bool> DeteleCategoryDish(CategoryDish categoryDish);

        Task<bool> UpdateCategoryDish(CategoryDish categoryDish);


    }
}
