using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class CategoryDishPrincipal
    {
        [Required(ErrorMessage = "El campo 'Nombre Categoria Plato' es requerido")]
        [MinLength(3, ErrorMessage = "El campo 'Nombre Categoria Plato' debe tener una longitud mínima de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El campo 'Nombre Categoria Plato' debe tener una longitud máxima de 50 caracteres")]
        public string NameCatDish { get; set; }
    }

    public class CategoryDishGet : CategoryDishPrincipal
    {
        public string Id { get; set; }
    }
}
