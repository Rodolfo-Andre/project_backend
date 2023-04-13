namespace project_backend.Models
{
    public class VoucherType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Voucher> Vouchers { get; set; }
    }
}
