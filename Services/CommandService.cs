using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class CommandService : ICommands
    {
        private readonly CommandsContext _context;

        public CommandService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCommand(Commands command)
        {
            bool result = false;

            try
            {
                _context.Commands.Add(command);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteCommand(Commands command)
        {
            bool result = false;

            try
            {
                _context.Commands.Remove(command);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<Commands> GetById(int id)
        {
            var command = await _context.Commands
                .Include(c => c.TableRestaurant)
                .Include(c => c.Employee)
                .Include(c => c.StatesCommand)
                .Include(c => c.DetailsComand).ThenInclude(d => d.Dish).ThenInclude(ca => ca.CategoryDish)
                .FirstOrDefaultAsync(c => c.Id == id);


            return command;
        }

        public async Task<List<Commands>> GetAll()
        {
            List<Commands> command = await _context.Commands
                .Include(c => c.TableRestaurant)
                .Include(c => c.Employee)
                .Include(c => c.StatesCommand)
                .Include(c => c.DetailsComand).ThenInclude(d => d.Dish).ThenInclude(ca => ca.CategoryDish)
                .ToListAsync();

            return command;
        }

        public async Task<bool> UpdateCommand(Commands command)
        {
            bool result = false;

            try
            {
                _context.Entry(command).State = EntityState.Modified;
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
