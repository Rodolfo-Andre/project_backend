using Mapster;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using System.Runtime.InteropServices;
using System.Windows.Input;



namespace project_backend.Services
{
    public class EstablishmentService : IEstablishment
    {


        private readonly CommandsContext _commandsContext;

        public EstablishmentService(CommandsContext commandsContext)
        {
            _commandsContext = commandsContext;
        }

        public async Task<Establishment> GetEstablishment(int id)
        {
            var payMethod = await _commandsContext.Establishment.FirstOrDefaultAsync(x => x.Id == id);
            return payMethod;

        }

        public async Task<List<Establishment>> GetEstablishments()
        {
            List<Establishment> listEstablishment = await _commandsContext.Establishment.ToListAsync();
            return listEstablishment;
        }

        public async Task<bool> updateEstblisment(Establishment establishment)
        {
            bool result = false;
            try
            {


                _commandsContext.Entry(establishment).State = EntityState.Modified;
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
