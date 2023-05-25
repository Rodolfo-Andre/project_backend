using project_backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class TableRestaurantPrincipal
    {
        [Required(ErrorMessage = "El campo 'Asientos' es requerido")]
        [Range(1, 9, ErrorMessage = "El valor de la propiedad 'Asientos' debe ser un número entero positivo no mayor a 9")]
        public int NumSeats { get; set; }
    }

    public class TableUpdate : TableRestaurantPrincipal
    {
        [EnumDataType(typeof(TypeTableState), ErrorMessage = "El valor proporcionado para 'Estado de Mesa' no es válido")]
        public TypeTableState StateTable { get; set; }
    }

    public class TableRestaurantGet : TableRestaurantPrincipal
    {
        public int NumTable { get; set; }
        public string StateTable { get; set; }
    }
}
