using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface IVoucherType
    {

        Task<List<VoucherTypeGet>> getAll();
        Task<VoucherType> getVoucherById(int id);
        Task<int> Create(VoucherType voucher);
        Task<int> Update(VoucherType voucher);
        Task<int> Delete(int id);
    }
}
