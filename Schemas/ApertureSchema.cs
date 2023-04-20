using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class AperturePrincipal
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FecAperture { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FecClose { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double SaleToDay { get; set; }
    }

    public class ApertureCreate : AperturePrincipal
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CashId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EmployeeId { get; set; }
    }


    public class ApertureUpdate : AperturePrincipal
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }
    }

    public class ApertureGet
    {
        public int Id { get; set; }
        public int CashId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime FecAperture { get; set; }
        public DateTime FecClose { get; set; }
        public double SaleToDay { get; set; }
    }
}
