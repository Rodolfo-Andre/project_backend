﻿using System.ComponentModel.DataAnnotations.Schema;

namespace project_backend.Models
{
    public class Commands
    {
        public int Id { get; set; }
        public int CantSeats { get; set; }
        public double PrecTotOrder { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Referencia clases
        public int TableRestaurantId { get; set; }
        public TableRestaurant TableRestaurant { get; set; }

        public int StatesCommandId { get; set; }
        public StatesCommand StatesCommand { get; set; }

        public Voucher? Voucher { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public List<DetailsComand> DetailsComand { get; set; } = new List<DetailsComand>();
    }
}
