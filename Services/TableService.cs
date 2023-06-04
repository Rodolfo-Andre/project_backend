using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class TableService : ITableRestaurant
    {
        private readonly CommandsContext _context;

        public TableService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTable(TableRestaurant table)
        {
            bool result = false;

            try
            {
                _context.TableRestaurant.Add(table);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteTable(TableRestaurant table)
        {
            bool result = false;

            try
            {
                _context.TableRestaurant.Remove(table);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<TableRestaurant> GetById(int id)
        {
            var table = await _context.TableRestaurant.FirstOrDefaultAsync(t => t.NumTable == id);

            return table;
        }

        public async Task<List<TableRestaurant>> GetAll()
        {
            var tables = await _context.TableRestaurant.ToListAsync();

            return tables;
        }

        public async Task<bool> UpdateTable(TableRestaurant tableUpdate)
        {
            bool result = false;

            try
            {
                _context.Entry(tableUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<int> GetNumberCommandsInTable(int idTable)
        {
            var table = await _context.TableRestaurant
            .Include(c => c.Commands)
            .Where(c => c.NumTable == idTable)
            .FirstOrDefaultAsync();

            return table.Commands.Count;
        }

    }
}
