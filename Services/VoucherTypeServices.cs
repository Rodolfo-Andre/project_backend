using Mapster;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Services
{
    public class VoucherTypeServices : IVoucherType
    {
        private readonly CommandsContext _context;

        public VoucherTypeServices(CommandsContext context)
        {
            this._context = context;
        }

        public async Task<int> Create(VoucherType voucher)
        {
            int result = -1;

            try
            {
                _context.VoucherType.Add(voucher);
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
                VoucherType voucherType = await getVoucherById(id);
                if (voucherType != null)
                {
                    _context.VoucherType.Remove(voucherType);
                    result = await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return result;
        }

        public Task<List<VoucherTypeGet>> getAll()
        {
            return _context.VoucherType.Include(x => x.Vouchers).Select(x => x.Adapt<VoucherTypeGet>()).ToListAsync();
        }

        public Task<VoucherType> getVoucherById(int id)
        {
            return _context.VoucherType.Include(x => x.Vouchers).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Update(VoucherType voucher)
        {
            int result = -1;

            try
            {

                _context.VoucherType.Update(voucher);
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
