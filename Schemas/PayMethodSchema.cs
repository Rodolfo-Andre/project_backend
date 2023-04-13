
using project_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class PayMethodDefault
    {
        [Required (ErrorMessage ="El campo {0} es requerido")]
        public string Paymethod { get; set; }
    }

  


    public class PayMethodGet {
        
        public int Id { get; set; } 
        public string Paymethod { get; set; }
    }
}
