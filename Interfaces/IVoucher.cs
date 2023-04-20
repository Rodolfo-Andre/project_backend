using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IVoucher
    {
        public Task<List<Voucher>> GetAll();
        public Task<Voucher> GetById(int id);
        public Task<bool> CreateVoucher(Voucher voucher);
        public Task<bool> UpdateVoucher(Voucher voucher);
        public Task<bool> DeleteVoucher(Voucher voucher);
    }
}
