using project_backend.Models;
using System.ComponentModel.DataAnnotations;


namespace project_backend.Schemas
{
    public class CommandSchema
    {
        [Required(ErrorMessage = "El campo 'Mesa' es requerido")]

        public int TableRestaurantId { get; set; }

        [Required(ErrorMessage = "La cantidad de asientos es requerida")]

        public int CantSeats { get; set; }
    }
    public class CommandGet : CommandSchema
    {
        public int Id { get; set; }
        public List<DetailCommandOrder> ListDetails { get; set; }


        public StateCommandSchema StatesCommand { get; set; }
        public UserPrincipal User { get; set; }
        public TableGet Table { get; set; }
    }
    public class CommandCreate : CommandSchema
    {
        public List<DetailCommandOrder> ListDetails { get; set; }
    }
    
}
