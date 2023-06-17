namespace project_backend.Dto.inputs
{
    public class CommandInput
    {
        public int id { get; set; }
        public int numTable { get; set; }
        public int cantPerson { get; set; }
        public int employeeId { get; set; }
        public decimal total { get; set; }
        public List<DishInput> listDish { get; set; }
    }

    public class DishInput
    {
        public string dishId { get; set; }
        public int quantity { get; set; }
        public string observation { get; set; }
    }
}
