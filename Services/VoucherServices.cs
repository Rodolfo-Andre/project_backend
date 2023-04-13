using Mapster;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Services
{
    public class VoucherServices : IVoucher
    {
        private readonly CommandsContext _context;

        public VoucherServices(CommandsContext context)
        {
            this._context = context;
        }
        public async Task<List<VoucherGet>> getAll()
        {
            return await _context.Voucher.Include(x => x.User).Include(x => x.Aperture).Include(x => x.VoucherType).Include(x => x.Establishment).Include(x => x.VoucherDetails).Select(e => e.Adapt<VoucherGet>()).ToListAsync();
        }

        public async Task<Voucher> getVoucherById(int id)
        {
            Voucher voucher = await _context.Voucher.Include(x => x.User).Include(x => x.Aperture).Include(x => x.VoucherType).Include(x => x.Establishment).Include(x => x.VoucherDetails).FirstOrDefaultAsync(x => x.Id == id);

            return voucher.Adapt<Voucher>();
        }

        public async Task<int> saveVoucher(Voucher voucher)
        {
            int result = -1;


            try
            {
                _context.Voucher.Add(voucher);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());


            }

            return result;
        }

        public async Task<int> updateVoucher(Voucher voucher)
        {
            int result = -1;


            try
            {
                _context.Voucher.Update(voucher);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());


            }

            return result;
        }

        public async Task<int> deleteVoucherById(int id)
        {
            int result = -1;

            Voucher voucher = await getVoucherById(id);

            if (voucher is null)
            {
                return result;
            }

            try
            {
                _context.Voucher.Remove(voucher);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());


            }

            return result;
        }
    }
}
