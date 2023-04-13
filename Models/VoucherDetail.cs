namespace project_backend.Models
{
    public class VoucherDetail
    {
        public int Id { get; set; }

        public int VoucherId { get; set; }
        public Voucher Voucher { get; set; }

        public int PayMethodId { get; set; }
        public PayMethod PayMethod { get; set; }

        public double PaymentAmount { get; set; }
    }
}
