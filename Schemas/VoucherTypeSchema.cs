using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class VoucherTypePrincipal
    {
        [Required(ErrorMessage = "El campo 'Tipo de Comprobante' es requerido")]
        [MinLength(3, ErrorMessage = "El campo 'Tipo de Comprobante' debe tener una longitud mínima de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El campo 'Tipo de Comprobante' debe tener una longitud máxima de 50 caracteres")]
        public string Name { get; set; }
    }

    public class VoucherTypeGet : VoucherTypePrincipal
    {
        public int Id { get; set; }
    }
}
