using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace project_backend.Services
{
    public class DetailsCommandService : IDetailsCommand
    {
        private readonly CommandsContext _commandsContext;
        public DetailsCommandService(CommandsContext commandsContext)
        {
            _commandsContext = commandsContext;
        }
        public async Task<bool> createDetailCommand(DetailsComand dc)
        {
            try
            {
                _commandsContext.DetailsComands.Add(dc);
                await _commandsContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> deleteDetailCommand(DetailsComand dc)
        {
            bool result = false;
            try
            {
                _commandsContext.DetailsComands.Remove(dc);
                await _commandsContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public async Task<List<DetailsComand>> getAllDetailsCommands()
        {
            List<DetailsComand> result = await _commandsContext.DetailsComands.ToListAsync();
            return result; ;
        }

        public async Task<List<DetailsComand>> GetDetailsComandsByNumCommand(int idCommand)
        {
            List<DetailsComand> result = await _commandsContext.DetailsComands.Where(d => d.CommandsId ==idCommand).ToListAsync();
            return result;
        }

        public async Task<DetailsComand> GetDetailsCommand(int idCommand, string idDish)
        {
            DetailsComand detail = await _commandsContext.DetailsComands.FirstOrDefaultAsync(d => d.CommandsId == idCommand && d.DishId == idDish);
            return detail;
        }

        public async Task<bool> updateDetailCommand(DetailsComand dc)
        {
            bool result = false;
            try
            {
                _commandsContext.Entry(dc).State = EntityState.Modified;
                await _commandsContext.SaveChangesAsync();
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
