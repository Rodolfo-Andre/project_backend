using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    /*
         public class CatDishPrincipal
         {
             [Required(ErrorMessage = "El campo 'Nombre Categoria Plato' es requerido")]
             [MinLength(5, ErrorMessage = "El campo 'El nombre de la categoria ' debe tener una longitud mínima de 5 caracteres")]
             [MaxLength(50, ErrorMessage = "El campo 'El nombre de la categoria ' debe tener una longitud máxima de 50 caracteres")]
             public string NameCatDish { get; set; }
         }


         public class CategoryDishPut : CatDishPrincipal {


         }
         public class CategoryDishGet : CatDishPrincipal
         { 
           public int Id { get; set; }
         }
     */

    using System.ComponentModel.DataAnnotations;

    namespace project_backend.Schemas
    {

        public class CatDishPrincipal
        {
            [Required(ErrorMessage = "El campo 'Nombre Categoria Plato' es requerido")]
            [MinLength(5, ErrorMessage = "El campo 'El nombre de la categoria ' debe tener una longitud mínima de 5 caracteres")]
            [MaxLength(50, ErrorMessage = "El campo 'El nombre de la categoria ' debe tener una longitud máxima de 50 caracteres")]
            public string NameCatDish { get; set; }
        }
        //para crear la categoria
        public class CategoryDishCreate : CatDishPrincipal
        {

        }
        //para modificar la categoria
        public class CategoryDishPut : CatDishPrincipal
        {

        }
        // para enviar informacion 
        public class CategoryDishGet : CatDishPrincipal
        {
            public string Id { get; set; }
        }

    }


}
