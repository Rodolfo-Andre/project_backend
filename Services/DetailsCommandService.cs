using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class DetailsCommandService : IDetailsCommand
    {
        private readonly CommandsContext _context;

        public DetailsCommandService(CommandsContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateDetailCommand(DetailsComand detailsCommand)
        {
            bool result = false;

            try
            {
                _context.DetailsComands.Add(detailsCommand);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteDetailCommand(DetailsComand detailsCommand)
        {
            bool result = false;

            try
            {
                _context.DetailsComands.Remove(detailsCommand);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<DetailsComand>> GetAll()
        {
            List<DetailsComand> listDetailsCommand = await _context.DetailsComands.ToListAsync();

            return listDetailsCommand; ;
        }

        public async Task<List<DetailsComand>> GetByCommandId(int idCommand)
        {
            List<DetailsComand> listDetailsCommand = await _context.DetailsComands
                .Where(d => d.CommandsId == idCommand)
                .ToListAsync();

            return listDetailsCommand;
        }

        public async Task<DetailsComand> GetByCommandIdAndDishId(int idCommand, string idDish)
        {
            DetailsComand detailsCommand = await _context.DetailsComands.FirstOrDefaultAsync(d => d.CommandsId == idCommand && d.DishId == idDish);

            return detailsCommand;
        }

        public async Task<bool> UpdateDetailCommand(DetailsComand detailsCommand)
        {
            bool result = false;

            try
            {
                _context.Entry(detailsCommand).State = EntityState.Modified;
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
