using project_backend.Interfaces;
using project_backend.Models;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Data;
using Mapster;
using project_backend.Schemas.project_backend.Schemas;

namespace project_backend.Services
{
    public class CategoryDishService : ICategoryDish
    {
        private readonly CommandsContext _CategoryDishContext;

        public CategoryDishService(CommandsContext commandsContext)
        {
            _CategoryDishContext = commandsContext;

        }

        public async Task<bool> CreateCategoryDish(CategoryDish categoryDish)
        {
            bool result = false;

            try
            {
                var listaCatDIsh = await _CategoryDishContext.CategoryDish.ToListAsync();

                categoryDish.Id = CategoryDish.GenerarIdCatDish(listaCatDIsh);

                _CategoryDishContext.CategoryDish.Add(categoryDish);
                await _CategoryDishContext.SaveChangesAsync();
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
                _CategoryDishContext.CategoryDish.Remove(categoryDish);
                await _CategoryDishContext.SaveChangesAsync();
                result = true;
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public async Task<List<CategoryDish>> GetCategoryDishs()
        {
           List<CategoryDish> categoryDishes = await _CategoryDishContext.CategoryDish
                .ToListAsync();

            return categoryDishes;
        }

        public async Task<CategoryDish> GetCategoryDish(string id)
        {
            var categoryDish = (await _CategoryDishContext.CategoryDish
                 .FirstOrDefaultAsync(cd => cd.Id == id));
                
            return categoryDish;    
        }

        public async Task<bool> UpdateCategoryDish(CategoryDish categoryDish)
        {
            bool result = false;

            try
            { 
                _CategoryDishContext.Entry(categoryDish).State = EntityState.Modified;
                await _CategoryDishContext.SaveChangesAsync();
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
