namespace project_backend.Dto
{
    public class PurchaseInformation
    {
        public string PayMethod { get; set; }
        public DateTime IssueDate { get; set; }
        public string CurrencyType { get; set; } = "Soles";
    }
}
