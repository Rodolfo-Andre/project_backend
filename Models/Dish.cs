
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace project_backend.Models
{
    public class Dish
    {
        public string Id { get; set; }

        public string NameDish { get; set; }

        public double PriceDish { get; set; }

        public string ImgDish { get; set; }

        public string CategoryDishId { get; set; }

        public CategoryDish CategoryDish { get; set; }

        public static string GenerarIdDish(List<Dish> listDish)
        {
            if (listDish.Count == 0) return "D-001";

            string ultimoId = listDish[listDish.Count - 1].Id;

            int numero = int.Parse(ultimoId.Split('-')[1]) + 1;

            return "D-" + numero.ToString("000");
        }
    }
}
