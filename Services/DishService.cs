
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using System.Windows.Input;


namespace project_backend.Services
{
    public class DishService : IDish

    {
        private readonly CommandsContext _Dishcontext;


        public DishService(CommandsContext context)
        {
            _Dishcontext = context;
        }

        public async Task<bool> CreateDish( Dish Dish)
        {
           bool result = false;
            try
            {
                var listaDish = await _Dishcontext.Dish.ToListAsync();

                Dish.Id = Dish.GenerarIdDish(listaDish);

                _Dishcontext.Dish.Add(Dish);
                await _Dishcontext.SaveChangesAsync();
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
                _Dishcontext.Dish.Remove(Dish);
                await _Dishcontext.SaveChangesAsync();
                result = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public async Task<List<Dish>> GetDishs()
        {
            List<Dish> dish = await _Dishcontext.Dish
                .Include(d=>d.CategoryDish)
                .ToListAsync();

            

            return dish;
                
        }

        public async Task<Dish> GetDish(string id)
        {
         var dish = await _Dishcontext.Dish
                 .Include(d => d.CategoryDish)
                .FirstOrDefaultAsync(d => d.Id == id);
            return dish;
        }

        public async Task<bool> UpdateDish(Dish  Dish)
        {
            bool result = false;
            try
            { 
                _Dishcontext.Entry(Dish).State = EntityState.Modified;
                await _Dishcontext.SaveChangesAsync();
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
