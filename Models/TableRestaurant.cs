using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_backend.Models
{
    public class TableRestaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumTable { get; set; }

        public int NumSeats { get; set; }

        public string StateTable { get; set; }

        public List<Commands> ListComand { get; set; }
    }
}
