using Mapster;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using System.Collections.Generic;

namespace project_backend.Services
{
    public class ApertureServices : IAperture
    {
        private readonly CommandsContext _context;

        public ApertureServices(CommandsContext context)
        {
            this._context = context;
        }



        public async Task<List<ApertureGet>> getAll()
        {

            return await _context.Aperture
            .Include(x => x.Employee).
            Include(x => x.Cash).
            Select(x => x.Adapt<ApertureGet>()).
            ToListAsync();
        }

        public async Task<Aperture> getApertureById(int id)
        {
            Aperture aperture = await _context.Aperture.Include(x => x.Employee).
            Include(x => x.Cash).FirstOrDefaultAsync(x => x.Id == id);
            return aperture;
        }

        public async Task<int> saveAperture(Aperture aperture)
        {
            int result = -1;

            try
            {
                _context.Aperture.Add(aperture);
                result = await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return result;

        }

        public async Task<int> updateAperture(Aperture aperture)
        {
            int result = -1;

            try
            {

                _context.Aperture.Update(aperture);
                result = await _context.SaveChangesAsync();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }


            return result;
        }

        public async Task<int> deleteAperture(int id)
        {
            int result = -1;

            try
            {
                Aperture aperture = await getApertureById(id);
                if (aperture != null)
                {
                    _context.Aperture.Remove(aperture);
                    result = await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
    }
}
