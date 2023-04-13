
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
    public class CashService : ICash
    {

        private readonly CommandsContext _commands;

        public CashService(CommandsContext cashCommand)
        {
            _commands = cashCommand;
        }


        public async Task<bool> createCash(Cash cash)
        {
           bool result=false;

            try
            {
                result = true;

                _commands.Cash.Add(cash);
                await _commands.SaveChangesAsync();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public async Task<bool> deleteCash(Cash cash)
        {
            bool result = false;
            try
            {
                
            
                _commands.Cash.Remove(cash);
                await _commands.SaveChangesAsync() ;
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public async Task<List<CashSchema>> getAllCash()
        {
            List<CashSchema> listCash = await _commands.Cash.Include(c => c.Establishment).Select(c=>c.Adapt<CashSchema>()).ToListAsync();

            return listCash;
        }

        public async Task<Cash> getCash(int id)
        {
            var cash = await _commands.Cash.Include(c => c.Establishment).FirstOrDefaultAsync(x => x.Id == id);


             return cash;
        }

        public async Task<bool> updateCash(Cash cash)
        {
            bool resul = false;
            try
            {
                _commands.Entry(cash).State=EntityState.Modified;
                await _commands.SaveChangesAsync();
                
                resul = true;

            }catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}"); 
            }
            return resul;
        }
    }
}
