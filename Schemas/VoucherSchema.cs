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
        public int idCommand { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int idTypeVoucher { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public ClientVOucher cliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CashId { get; set; }
        public List<DetailPayment> listPayment { get; set; }
        public double subTotal { get; set; }
        public double total { get; set; }
        public int idCash { get; set; }

        public int idEmployee { get; set; }
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


    public class ClientVOucher
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string dni { get; set;}
    }


    public class DetailPayment
    {
        public int idTypePayment { get; set; }

        public double amount { get; set; }
    }
}
