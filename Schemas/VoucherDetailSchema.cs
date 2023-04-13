using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class VoucherDetailDefault
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int VoucherId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int PayMethodId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public double PaymentAmount { get; set; }

    }


    public class VoucherDetailCreate : VoucherDetailDefault
    {
    }


    public class VoucherDetailUpdate : VoucherDetailDefault
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }
    }


    public class VoucherDetailGet
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public string PayMethodId { get; set; }
        public double PaymentAmount { get; set; }
    }
}
