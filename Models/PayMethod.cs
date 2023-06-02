
namespace project_backend.Models
{
    public class PayMethod
    {
        public int Id { get; set; }
        public string Paymethod { get; set; }

        public List<VoucherDetail> VouchersDetails { get; } = new List<VoucherDetail>();
    }
}
