namespace project_backend.Models
{
    public class StatesCommand
    {
        public int Id { get; set; }
        public string State { get; set; }

        public List<Commands> Commands { get; } = new List<Commands>();
    }
}
