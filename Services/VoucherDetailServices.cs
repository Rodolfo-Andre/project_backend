using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class VoucherDetailServices : IVoucherDetail
    {
        private readonly CommandsContext _context;

        public VoucherDetailServices(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateVoucherDetail(VoucherDetail voucherDetail)
        {
            bool result = false;

            try
            {
                _context.VoucherDetail.Add(voucherDetail);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteVoucherDetail(VoucherDetail voucherDetail)
        {
            bool result = false;

            try
            {
                _context.VoucherDetail.Remove(voucherDetail);
                await _context.SaveChangesAsync();

                result = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<VoucherDetail>> GetAll()
        {
            return await _context.VoucherDetail
                .Include(x => x.PayMethod)
                .Include(x => x.Voucher)
                .ToListAsync();
        }

        public async Task<VoucherDetail> GetById(int id)
        {
            VoucherDetail voucherDetail = await _context.VoucherDetail
                .Include(x => x.PayMethod)
                .Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.Id == id);

            return voucherDetail;
        }

        public async Task<bool> UpdateVoucherDetail(VoucherDetail voucherDetail)
        {
            bool result = false;

            try
            {
                _context.Entry(voucherDetail).State = EntityState.Modified;
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
