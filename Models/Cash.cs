using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace project_backend.Models
{
    public class Cash
    {
        public int Id { get; set; }

        public int EstablishmentId { get; set; }

        public Establishment Establishment { get; set; }
    }
}
