using project_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class VoucherPrincipal
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener como minimo {2} y como maximo {1} caracteres")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime DateIssued { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int NumCom { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double TotalPrice { get; set; }
    }

    public class VoucherCreate : VoucherPrincipal

    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EstablishmentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int VoucherTypeId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ApertureId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CommandId { get; set; }

        public int TableRestaurantId { get; set; }
    }

    public class VoucherUpdate : VoucherCreate
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }
    }

    public class VoucherGet
    {
        public int Id { get; set; }
        public int EstablishmentId { get; set; }
        public string CustomerName { get; set; }
        public int VoucherTypeId { get; set; }
        public DateTime DateIssued { get; set; }

        public int UserId { get; set; }
        public int NumCom { get; set; }
        public double TotalPrice { get; set; }
        public int ApertureId { get; set; }

        public List<VoucherDetail> VoucherDetails { get; set; }
    }
}
