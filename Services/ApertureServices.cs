using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class ApertureServices : IAperture
    {
        private readonly CommandsContext _context;

        public ApertureServices(CommandsContext context)
        {
            _context = context;
        }

        public async Task<List<Aperture>> GetAll()
        {
            return await _context.Aperture
            .Include(x => x.Employee)
            .Include(x => x.Cash)
            .ToListAsync();
        }

        public async Task<Aperture> GetById(int id)
        {
            Aperture aperture = await _context.Aperture
                .Include(x => x.Employee)
                .Include(x => x.Cash)
                .FirstOrDefaultAsync(x => x.Id == id);

            return aperture;
        }

        public async Task<bool> CreateAperture(Aperture aperture)
        {
            bool result = false;

            try
            {
                _context.Aperture.Add(aperture);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> UpdateAperture(Aperture aperture)
        {
            bool result = false;

            try
            {
                _context.Entry(aperture).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteAperture(Aperture aperture)
        {
            bool result = false;

            try
            {
                _context.Aperture.Remove(aperture);
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
