using Mapster;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Services
{
    public class VoucherDetailServices : IVoucherDetail
    {
        private readonly CommandsContext _context;

        public VoucherDetailServices(CommandsContext context)
        {
            this._context = context;
        }

        public async Task<int> Create(VoucherDetail voucher)
        {
            int result = -1;

            try
            {
                _context.VoucherDetail.Add(voucher);
                result = await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = -1;

            try
            {
                VoucherDetail voucherDetail = await getVoucherById(id);
                if (voucherDetail != null)
                {
                    _context.VoucherDetail.Remove(voucherDetail);
                    result = await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        public async Task<List<VoucherDetailGet>> getAll()
        {
            return await _context.VoucherDetail.Include(x => x.PayMethod).Include(x => x.Voucher).Select(x => x.Adapt<VoucherDetailGet>()).ToListAsync();
        }

        public async Task<VoucherDetail> getVoucherById(int id)
        {
            VoucherDetail voucherDetail = await _context.VoucherDetail.Include(x => x.PayMethod).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Id == id);
            return voucherDetail;
        }

        public async Task<int> Update(VoucherDetail voucher)
        {
            int result = -1;

            try
            {
                _context.VoucherDetail.Update(voucher);
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
