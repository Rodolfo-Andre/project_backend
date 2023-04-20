using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IVoucherType
    {
        public Task<List<VoucherType>> GetAll();
        public Task<VoucherType> GetById(int id);
        public Task<bool> CreateVoucherType(VoucherType voucherType);
        public Task<bool> UpdateVoucherType(VoucherType voucherType);
        public Task<bool> DeleteVoucherType(VoucherType voucherType);
    }
}
