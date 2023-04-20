namespace project_backend.Models
{
    public class DetailsComand
    {
        public int Id { get; set; }
        public int CantDish { get; set; }
        //Precio del plato por si cambia para después
        public double PrecDish { get; set; }
        public double PrecOrder { get; set; }
        public string Observation { get; set; }

        //Agregar referencias
        public Commands Commands { get; set; }
        public int CommandsId { get; set; }

        public Dish Dish { get; set; }
        public string DishId { get; set; }
    }
}
