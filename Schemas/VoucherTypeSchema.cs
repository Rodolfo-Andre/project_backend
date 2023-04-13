using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class VoucherTypeDefault
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

    }


    public class VoucherTypeCreate : VoucherTypeDefault
    {
    }

    public class VoucherTypeUpdate : VoucherTypeDefault
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }
    }

    public class VoucherTypeGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
