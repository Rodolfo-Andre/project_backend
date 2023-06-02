using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class VoucherServices : IVoucher
    {
        private readonly CommandsContext _context;

        public VoucherServices(CommandsContext context)
        {
            _context = context;
        }

        public async Task<List<Voucher>> GetAll()
        {
            return await _context.Voucher
                .Include(x => x.Employee)
                .Include(x => x.Cash)
                .Include(x => x.Customer)
                .Include(x => x.VoucherType)
                .Include(x => x.Establishment)
                .Include(x => x.VoucherDetails)
                .ThenInclude(x => x.PayMethod)
                .Include(x => x.Commands)
                .ThenInclude(x => x.DetailsComand)
                .ToListAsync();
        }

        public async Task<Voucher> GetById(int id)
        {
            Voucher voucher = await _context.Voucher
                .Include(x => x.Employee)
                .Include(x => x.Cash)
                .Include(x => x.Customer)
                .Include(x => x.VoucherType)
                .Include(x => x.Establishment)
                .Include(x => x.VoucherDetails)
                .ThenInclude(x => x.PayMethod)
                .Include(x => x.Commands)
                .ThenInclude(x => x.DetailsComand)
                .ThenInclude(x => x.Dish)
                .FirstOrDefaultAsync(x => x.Id == id);

            return voucher;
        }

        public async Task<bool> CreateVoucher(Voucher voucher)
        {
            bool result = false;

            try
            {
                _context.Voucher.Add(voucher);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> UpdateVoucher(Voucher voucher)
        {
            bool result = false;

            try
            {
                _context.Entry(voucher).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteVoucher(Voucher voucher)
        {
            bool result = false;

            try
            {
                _context.Voucher.Remove(voucher);
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
