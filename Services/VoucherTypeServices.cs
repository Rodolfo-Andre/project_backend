using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class VoucherTypeServices : IVoucherType
    {
        private readonly CommandsContext _context;

        public VoucherTypeServices(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateVoucherType(VoucherType voucherType)
        {
            bool result = false;

            try
            {
                _context.VoucherType.Add(voucherType);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteVoucherType(VoucherType voucherType)
        {
            bool result = false;

            try
            {
                _context.VoucherType.Remove(voucherType);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<VoucherType>> GetAll()
        {
            return await _context.VoucherType
                .Include(x => x.Vouchers)
                .ToListAsync();
        }

        public Task<VoucherType> GetById(int id)
        {
            return _context.VoucherType.Include(x => x.Vouchers).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateVoucherType(VoucherType voucherType)
        {
            bool result = false;

            try
            {
                _context.Entry(voucherType).State = EntityState.Modified;
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
