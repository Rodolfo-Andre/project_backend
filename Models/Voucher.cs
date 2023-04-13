using System.ComponentModel.DataAnnotations;

namespace project_backend.Models
{
    public class Voucher
    {
        public int Id { get; set; }

        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }

        public string CustomerName { get; set; }
        public int VoucherTypeId { get; set; }
        public VoucherType VoucherType { get; set; }

        public DateTime DateIssued { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int NumCom { get; set; }
        public double TotalPrice { get; set; }

        public int ApertureId { get; set; }
        public Aperture Aperture { get; set; }
        public List<VoucherDetail> VoucherDetails { get; set; }
    }
}
