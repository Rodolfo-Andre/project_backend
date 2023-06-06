using Microsoft.Extensions.Hosting;
using project_backend.Interfaces;

namespace project_backend.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public DateTime DateIssued { get; set; }
        public double TotalPrice { get; set; }
        public double Igv { get; set; }
        public double Discount { get; set; }

        public int CommandsId { get; set; }
        public Commands Commands { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int VoucherTypeId { get; set; }
        public VoucherType VoucherType { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int CashId { get; set; }
        public Cash Cash { get; set; }

        public List<VoucherDetail> VoucherDetails { get; } = new List<VoucherDetail>();
    }
}
