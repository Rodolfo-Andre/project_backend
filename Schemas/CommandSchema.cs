using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class CommandPrincipal
    {
        [Required(ErrorMessage = "El campo 'Mesa' es requerido")]
        public int TableRestaurantId { get; set; }

        [Required(ErrorMessage = "La cantidad de asientos es requerida")]
        public int CantSeats { get; set; }
    }

    public class CommandGet : CommandPrincipal
    {
        public int Id { get; set; }
        public List<DetailCommandOrder> ListDetails { get; set; }

        public StateCommandSchema StatesCommand { get; set; }
        public UserPrincipal User { get; set; }
        public TableGet Table { get; set; }
    }

    public class CommandCreate : CommandPrincipal
    {
        public List<DetailCommandOrder> ListDetails { get; set; }
    }
}
