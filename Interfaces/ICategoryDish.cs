using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface ICategoryDish
    {
        public Task<List<CategoryDish>> GetAll();
        public Task<CategoryDish> GetById(string id);
        public Task<bool> CreateCategoryDish(CategoryDish categoryDish);
        public Task<bool> DeteleCategoryDish(CategoryDish categoryDish);
        public Task<bool> UpdateCategoryDish(CategoryDish categoryDish);
        public Task<int> GetNumberDishInCategoryDish(String idCategoryDish);
        public Task<bool> IsNameCatDishUnique(string nameCatDish, string idCategoryDish = null);
    }
}
