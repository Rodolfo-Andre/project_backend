using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class DetailCommandPrincipal
    {
        public int IdCommand { get; set; }
    }

    public class DetailCommandGet : DetailCommandPrincipal
    {
        public DishPrincipal Dish { get; set; }
        public int cantDish { get; set; }
        public double priceOrder { get; set; }
    }

    public class DetailCommandCreate : DetailCommandPrincipal
    {
        [Required(ErrorMessage = "El campo 'Plato' es requerido")]
        public string DishId { get; set; }

        [Required(ErrorMessage = "El campo 'Cantidad de Platos' es requerido")]
        [Range(1, 9, ErrorMessage = "La cantidad debe ser un número entero positivo")]
        public int cantDish { get; set; }

        public string Observation { get; set; }
    }

    public class DetailCommandOrder
    {
        public string DishId { get; set; }
        public int cantDish { get; set; }
        public string Observation { get; set; }
    }

    public class DetailCommandUpdate : DetailCommandPrincipal
    {
        [Required(ErrorMessage = "El campo 'Cantidad de Platos' es requerido")]
        [Range(1, 9, ErrorMessage = "La cantidad debe ser un número entero positivo")]
        public int cantDish { get; set; }
    }
}
