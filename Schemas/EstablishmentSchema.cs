
using project_backend.Models;
using System.ComponentModel.DataAnnotations;
namespace project_backend.Schemas
{

    public class EstablishmentPrincipal
    {
        [Required(ErrorMessage = "El campo 'Nombre Establecimiento' es requerido")]
        [MinLength(10, ErrorMessage = "El campo 'Nombre Establecimiento' debe tener una longitud mínima de 10 caracteres")]
        [MaxLength(50, ErrorMessage = "El campo 'Nombre Establecimiento' debe tener una longitud máxima de 50 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo 'Telefono Establecimiento' es requerido")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El campo 'Direccion Establecimiento' es requerido")]
        public string Address { get; set; }
        [Required(ErrorMessage = "El campo 'Ruc Establecimiento' es requerido")]
        public string Ruc { get; set; }
    }

  
  
}
