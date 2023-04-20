
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class CashService : ICash
    {
        private readonly CommandsContext _context;

        public CashService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCash(Cash cash)
        {
            bool result = false;

            try
            {
                _context.Cash.Add(cash);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteCash(Cash cash)
        {
            bool result = false;

            try
            {
                _context.Cash.Remove(cash);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<Cash>> GetAll()
        {
            List<Cash> listCash = await _context.Cash.Include(c => c.Establishment).ToListAsync();

            return listCash;
        }

        public async Task<Cash> GetById(int id)
        {
            var cash = await _context.Cash.Include(c => c.Establishment)
                .FirstOrDefaultAsync(x => x.Id == id);

            return cash;
        }

        public async Task<bool> UpdateCash(Cash cash)
        {
            bool result = false;

            try
            {
                _context.Entry(cash).State = EntityState.Modified;
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
