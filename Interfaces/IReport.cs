using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IReport
    {
        public Task<byte[]> ReportVoucher(Voucher voucher);
    }
}
