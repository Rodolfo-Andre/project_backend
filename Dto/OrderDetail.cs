namespace project_backend.Dto
{
    public class OrderDetail
    {
        public int Quantity { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string UnitOfMeasure { get; set; } = "UNIDADES";
        public double Amount { get; set; }
    }
}
