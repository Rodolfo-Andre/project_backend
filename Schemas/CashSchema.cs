
using project_backend.Models;
using System.ComponentModel.DataAnnotations;



namespace project_backend.Schemas
{
    public class CashSchema
    {
        [Required(ErrorMessage = "El campo 'Id Establecimiento' es necesario")]
        public int Id { get; set; }
        public int EstablishmentId { get; set; }
    }
}
