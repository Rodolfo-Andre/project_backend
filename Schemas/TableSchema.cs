using System.ComponentModel.DataAnnotations;


namespace project_backend.Schemas
{
    public class TableSchema
    {

        public int numTable { get; set; }

    }
    public class TableCreate
    {
        [Required(ErrorMessage = "La cantidad de asientos es requerido")]
        [Range(1, 9, ErrorMessage = "El valor de la propiedad Numero debe ser un número entero positivo")]
        public int numSeats { get; set; }


    }
    public class TableUpdate
    {

        [Required(ErrorMessage = "La cantidad de asientos es requerido")]
        [Range(1, 9, ErrorMessage = "El valor de la propiedad Numero debe ser un número entero positivo")]
        public int numSeats { get; set; }

        public string StateTable { get; set; }

    }
    public class TableGet : TableSchema
    {
        public int numSeats { get; set; }
        public string StateTable { get; set; }
    }
 
}
