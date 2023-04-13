using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_backend.Models
{
    public class PayMethod
    {
        public int Id { get; set; }

        public string Paymethod { get; set; }
    }
}
