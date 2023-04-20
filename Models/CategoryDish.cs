namespace project_backend.Models
{
    public class CategoryDish
    {
        public string Id { get; set; }
        public string NameCatDish { get; set; }

        public List<Dish> Dish { get; set; }

        public static string GenerateId(List<CategoryDish> listCatDish)
        {
            if (listCatDish.Count == 0) return "C-001";

            string ultimoId = listCatDish[listCatDish.Count - 1].Id;

            int numero = int.Parse(ultimoId.Split('-')[1]) + 1;

            return "C-" + numero.ToString("000");
        }
    }
}
