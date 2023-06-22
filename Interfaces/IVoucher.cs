using project_backend.Dto;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface IVoucher
    {
        public Task<List<Voucher>> GetAll();
        public Task<Voucher> GetById(int id);
        public Task<bool> CreateVoucher(VoucherCreate voucher);
        public Task<bool> UpdateVoucher(Voucher voucher);
        public Task<bool> DeleteVoucher(Voucher voucher);
        public Task<List<SalesDataPerDate>> GetSalesDataPerDate();
    }
}
