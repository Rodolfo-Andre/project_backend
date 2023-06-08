namespace project_backend.Dto
{
    public class SalesDataPerDate
    {
        public DateTime DateIssued { get; set; }
        public double AccumulatedSales { get; set; }
        public int NumberOfGeneratedReceipts { get; set; }
        public int QuantityOfDishSales { get; set; }
        public string BestSellingDish { get; set; }
    }
}
