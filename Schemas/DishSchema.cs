using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class DishPrincipal
    {
        [Required(ErrorMessage = "El campo 'Nombre Plato' es requerido")]
        [MinLength(3, ErrorMessage = "El campo 'Nombre Plato' debe tener una longitud mínima de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El campo 'Nombre Plato' debe tener una longitud máxima de 50 caracteres")]
        public string NameDish { get; set; }

        [Required(ErrorMessage = "El campo 'Precio Plato' es requerido")]
        public double PriceDish { get; set; }

        [Required(ErrorMessage = "El campo 'Imagen' es requerido")]
        public string ImgDish { get; set; }
    }

    public class DishCreateOrUpdate : DishPrincipal
    {
        [Required(ErrorMessage = "El campo 'Categoría' es requerido")]
        public string CategoryDishId { get; set; }
    }

    public class DishGet : DishPrincipal
    {
        public string Id { get; set; }
        public CategoryDishGet CategoryDish { get; set; }
    }
}
