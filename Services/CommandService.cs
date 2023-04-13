using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using System.Windows.Input;

namespace project_backend.Services
{
    public class CommandService : ICommands
    {
        private readonly CommandsContext _commandsContext;
        

        public CommandService(CommandsContext commandsContext)
        {
            _commandsContext = commandsContext;
          
        }
        public async Task<bool> createCommand(Commands command)
        {
            bool result = false;
            try
            {
                _commandsContext.Commands.Add(command);
                await _commandsContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
               
            }
            return result;
        }

        public async Task<bool> deleteCommand(Commands command)
        {
            bool result = false;
            try
            {
                _commandsContext.Commands.Remove(command);
                await _commandsContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public async Task<Commands> getComand(int id)
        {
            var command = await _commandsContext.Commands
   
                .Include(c => c.DetailsComand)
                .FirstOrDefaultAsync(c => c.Id == id);
            return command;
        }

        public async Task<List<Commands>> getCommands()
        {
            List<Commands> command = await _commandsContext.Commands
                .Include(c => c.StatesCommand)
                .Include(c => c.TableRestaurant)
                .Include(c => c.DetailsComand)
                .ToListAsync();
            return command;
        }

        public async Task<bool> updateCommand(Commands command)
        {
            bool result = false;
            try
            {
                _commandsContext.Entry(command).State = EntityState.Modified;
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
