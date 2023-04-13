using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class DetailCommandSchema
    {
        public int IdCommand { get; set; }

        
    }
    public class DetailCommandGET : DetailCommandSchema 
    { 
        public DishPrincipal Dish { get; set; }
        public int cantDish { get; set; }
        public double priceOrder { get; set; }
        
        
    }
    public class DetailCommandCreate : DetailCommandSchema
    {
        [Required(ErrorMessage = "El campo 'Plato' es requerido")]
        public DishOrder Dish { get; set; }
        [Required(ErrorMessage = "El campo 'cantidad' es requerido")]
        [Range(1, 9, ErrorMessage = "La cantidad debe ser un número entero positivo")]
        public int cantDish { get; set; }
        public string Observation { get; set; }


    }

    public class DetailCommandOrder
    {
        public DishOrder Dish { get; set; }
        public int cantDish { get; set; }
        public string Observation { get; set; }
    }
    public class DetailCommandEdit : DetailCommandSchema
    {
        

       
        [Required(ErrorMessage = "El campo 'cantidad' es requerido")]
        [Range(1, 9, ErrorMessage = "La cantidad debe ser un número entero positivo")]
        public int cantDish { get; set; }

    }
}
