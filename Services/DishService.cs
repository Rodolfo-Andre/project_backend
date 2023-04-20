
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class DishService : IDish
    {
        private readonly CommandsContext _context;

        public DishService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDish(Dish Dish)
        {
            bool result = false;

            try
            {
                var listDish = await _context.Dish.ToListAsync();

                Dish.Id = Dish.GenerateId(listDish);
                _context.Dish.Add(Dish);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeteleDish(Dish Dish)
        {
            bool result = false;

            try
            {
                _context.Dish.Remove(Dish);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<Dish>> GetAll()
        {
            List<Dish> listDish = await _context.Dish
                .Include(d => d.CategoryDish)
                .ToListAsync();

            return listDish;
        }

        public async Task<Dish> GetById(string id)
        {
            var dish = await _context.Dish
                    .Include(d => d.CategoryDish)
                   .FirstOrDefaultAsync(d => d.Id == id);

            return dish;
        }

        public async Task<bool> UpdateDish(Dish Dish)
        {
            bool result = false;

            try
            {
                _context.Entry(Dish).State = EntityState.Modified;
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