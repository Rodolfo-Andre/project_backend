using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface IVoucherDetail
    {
        Task<List<VoucherDetailGet>> getAll();
        Task<VoucherDetail> getVoucherById(int id);
        Task<int> Create(VoucherDetail voucher);
        Task<int> Update(VoucherDetail voucher);
        Task<int> Delete(int id);
    }
}
