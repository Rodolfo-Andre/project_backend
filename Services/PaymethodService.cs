using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class PaymethodService : IPayMethod
    {
        private readonly CommandsContext _context;

        public PaymethodService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePaymethod(PayMethod payMethod)
        {
            bool result = false;

            try
            {
                _context.PayMethods.Add(payMethod);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> UpdatePaymethod(PayMethod payMethod)
        {
            bool result = false;

            try
            {
                _context.Entry(payMethod).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeletePaymethod(PayMethod payMethod)
        {
            bool result = false;

            try
            {
                _context.PayMethods.Remove(payMethod);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<PayMethod> GetById(int id)
        {
            var payMethod = await _context.PayMethods.FirstOrDefaultAsync(x => x.Id == id);

            return payMethod;
        }

        public async Task<List<PayMethod>> GetAll()
        {
            return await _context.PayMethods.ToListAsync();
        }

        public async Task<int> GetNumberVouchersDetailsInPayMethod(int idPayMethod)
        {
            var payMethod = await _context.PayMethods
               .Include(c => c.VouchersDetails)
               .Where(c => c.Id == idPayMethod)
               .FirstOrDefaultAsync();

            return payMethod.VouchersDetails.Count;
        }
    }
}

