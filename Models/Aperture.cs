namespace project_backend.Models
{
    public class Aperture
    {
        public int Id { get; set; }

        public int CashId { get; set; }
        public Cash Cash { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime FecAperture { get; set; }
        public DateTime FecClose { get; set; }
        public double SaleToDay { get; set; }

        public ICollection<Voucher> Vouchers { get; set; }
    }
}
