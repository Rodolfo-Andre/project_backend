using project_backend.Interfaces;
using project_backend.Models;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;

namespace project_backend.Services
{
    public class CategoryDishService : ICategoryDish
    {
        private readonly CommandsContext _context;

        public CategoryDishService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCategoryDish(CategoryDish categoryDish)
        {
            bool result = false;

            try
            {
                var listCategoryDish = await _context.CategoryDish.ToListAsync();

                categoryDish.Id = CategoryDish.GenerateId(listCategoryDish);
                _context.CategoryDish.Add(categoryDish);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeteleCategoryDish(CategoryDish categoryDish)
        {
            bool result = false;

            try
            {
                _context.CategoryDish.Remove(categoryDish);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<CategoryDish>> GetAll()
        {
            List<CategoryDish> categoryDishes = await _context.CategoryDish
                 .ToListAsync();

            return categoryDishes;
        }

        public async Task<CategoryDish> GetById(string id)
        {
            var categoryDish = await _context.CategoryDish
                 .FirstOrDefaultAsync(c => c.Id == id);

            return categoryDish;
        }

        public async Task<bool> UpdateCategoryDish(CategoryDish categoryDish)
        {
            bool result = false;

            try
            {
                _context.Entry(categoryDish).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }
    }
}
