using System.ComponentModel.DataAnnotations;

namespace Inter.Models
{
    public class Status
    {
        public int Id { get; set; }

        [MaxLength(90)]
        public string Description { get; set; }

    }
}
