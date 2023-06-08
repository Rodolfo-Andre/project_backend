
using Castle.Core.Resource;
using Mapster;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Dto;
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

        public async Task<int> GetNumberDetailsCommandsInDish(String idDish)
        {
            var dish = await _context.Dish
               .Include(c => c.DetailsComands)
               .Where(c => c.Id == idDish)
               .FirstOrDefaultAsync();

            return dish.DetailsComands.Count;
        }

        public async Task<List<DishOrderStatistics>> GetDishOrderStatistics()
        {
            var query = from dc in _context.DetailsComands
                        join c in _context.Commands on dc.CommandsId equals c.Id
                        join d in _context.Dish on dc.DishId equals d.Id
                        join ct in _context.CategoryDish on d.CategoryDishId equals ct.Id
                        where c.StatesCommandId == 3
                        group dc by new { dc.DishId, d.NameDish, d.ImgDish, ct.NameCatDish } into g
                        orderby g.Sum(dc => dc.PrecOrder) descending
                        select new
                        {
                            g.Key.DishId,
                            g.Key.NameDish,
                            g.Key.ImgDish,
                            g.Key.NameCatDish,
                            TotalSales = g.Sum(dc => dc.PrecOrder),
                            QuantityOfDishesSold = g.Sum(dc => dc.CantDish)
                        };

            var result = await query.ToListAsync();

            return result.Adapt<List<DishOrderStatistics>>();
        }
    }
}