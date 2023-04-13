using project_backend.Schemas.project_backend.Schemas;
using System.ComponentModel.DataAnnotations;


namespace project_backend.Schemas
{

    public class DishPrincipal
    {
        [Required(ErrorMessage = "El campo 'Nombre Plato' es requerido")]
        [MinLength(10, ErrorMessage = "El campo 'Nombre Plato' debe tener una longitud mínima de 10 caracteres")]
        [MaxLength(50, ErrorMessage = "El campo 'Nombre Plato' debe tener una longitud máxima de 50 caracteres")]
        public string NameDish { get; set; }
        [Required(ErrorMessage = "El campo 'Precio Plato' es requerido")]
        public double PriceDish { get; set; }

        public string ImgDish { get; set; }


    }
    //para crear el plato
    public class DishCreate : DishPrincipal
    {

        public string CategoryDishId { get; set; }

    }
    //para modificar el plato
    public class DishPut : DishPrincipal
    {
        public string CategoryDishId { get; set; }

    }

    public class DishOrder
    {
        public string Id { get; set; }
    }

    //para utilizar el enviar informacion
    public class DishGet : DishPrincipal
    {
        public string Id { get; set; }

        public CategoryDishGet CategoryDish { get; set; }


    }
}
