using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class TableService : ITableRestaurant
    {
        private readonly CommandsContext _commandsContext;
        public TableService(CommandsContext commandsContext) 
        { 
            _commandsContext = commandsContext;
        }

        public async Task<bool> createTable(TableRestaurant table)
        {
            bool result = false;
            try
            {
                _commandsContext.TableRestaurant.Add(table);
                await _commandsContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;

        }

        public async Task<bool> DeleteStateTable(TableRestaurant table)
        {
            bool result = false;
            try
            {
                _commandsContext.TableRestaurant.Remove(table);
                await _commandsContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public async Task<TableRestaurant> GetTableById(int id)
        {
            TableRestaurant tableRestaurant = await _commandsContext.TableRestaurant.FirstOrDefaultAsync(t => t.NumTable == id);
            return tableRestaurant;
        }

        public async Task<List<TableRestaurant>> GetTables()
        {
            List<TableRestaurant> tables = await _commandsContext.TableRestaurant.ToListAsync();
            return tables;
        }

        public async Task<bool> UpdateStateTable(TableRestaurant tableUpdate)
        {
            bool result = false;
            try
            {
                _commandsContext.Entry(tableUpdate).State = EntityState.Modified;
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
