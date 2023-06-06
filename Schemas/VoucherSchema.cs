using project_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class VoucherPrincipal
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime DateIssued { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double TotalPrice { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double Igv { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double Discount { get; set; }
    }

    public class VoucherCreate : VoucherPrincipal
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int VoucherTypeId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CommandsId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CashId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CustomerId { get; set; }

        public int TableRestaurantId { get; set; }
    }

    public class VoucherUpdate : VoucherCreate
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }
    }

    public class VoucherGet : VoucherPrincipal
    {
        public int Id { get; set; }
        public CommandGet Commands { get; set; }
        public CustomerGet Customer { get; set; }
        public VoucherTypeGet VoucherType { get; set; }
        public EmployeeGet Employee { get; set; }
        public CashGet Cash { get; set; }

        public List<VoucherDetailGet> VoucherDetails { get; set; }
    }
}
