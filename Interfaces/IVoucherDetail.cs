using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IVoucherDetail
    {
        public Task<List<VoucherDetail>> GetAll();
        public Task<VoucherDetail> GetById(int id);
        public Task<bool> CreateVoucherDetail(VoucherDetail voucherDetail);
        public Task<bool> UpdateVoucherDetail(VoucherDetail voucherDetail);
        public Task<bool> DeleteVoucherDetail(VoucherDetail voucherDetail);
    }
}
