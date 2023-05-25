using project_backend.Models;
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

    public class CommandGet
    {
        public int Id { get; set; }
        public List<DetailCommandGetOrder> DetailsComand { get; set; }

        public StateCommandSchema StatesCommand { get; set; }
        public UserComandGet User { get; set; }
        public TableRestaurantGet TableRestaurant { get; set; }
    }

    public class CommandCreate : CommandPrincipal
    {
        public List<DetailCommandOrder> ListDetails { get; set; }
    }
}
