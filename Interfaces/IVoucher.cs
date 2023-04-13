using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface IVoucher
    {
        Task<List<VoucherGet>> getAll();
        Task<Voucher> getVoucherById(int id);
        Task<int> saveVoucher(Voucher voucher);

        Task<int> updateVoucher(Voucher voucher);
        Task<int> deleteVoucherById(int id);

    }
}
