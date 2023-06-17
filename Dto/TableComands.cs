namespace project_backend.Dto
{
    public class TableComands
    {
        public int NumTable { get; set; }
        public int NumSeats { get; set; }
        public string StateTable { get; set; }
        public List<CommandsCustom> Commands { get; set; }
        public  CommandsCustom commandActive { get; set; }
        public bool hasCommands { get; set; }
    }


    public class CommandsCustom
    {
        public int Id { get; set; }
        public int CantSeats { get; set; }
        public double PrecTotOrder { get; set; }
        public string CreatedAt { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int StatesCommandId { get; set; }
        public string StatesCommandName { get;set; }
        public List<DetailCommandCustom> DetailsComand { get; set; }
    }

    public class GetCommandWithTable : CommandsCustom
    {

        public int NumTable { get; set; }
        public int NumSeats { get; set; }
        public string StateTable { get; set; }
        public bool isCommandActive { get; set; }
    }


      

    public class DetailCommandCustom {
        public int Id { get; set; }
        public int CantDish { get; set; }
        public double PrecDish { get; set; }
        public double PrecOrder { get; set; }
        public string Observation { get; set; }
        public DishCustom Dish { get; set; }
    }


    public class DishCustom {
        public string Id { get; set; }
        public string NameDish { get; set; }
        public double PriceDish { get; set; }
        public string ImgDish { get; set; }
        public string CategoryDishId { get; set; }
        public string CategoryDishName { get; set; }
        
    }

}
