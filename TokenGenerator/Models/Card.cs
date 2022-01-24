using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TokenGenerator.Utils;

namespace TokenGenerator.Models
{
    public class Card
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [NumberLenght(16, 16)]
        public long Number { get; set; }
        public int CostumerId { get; set; }
        [NotMapped]
        // This field is only used during the processing of the request and will not be stored
        [NumberLenght(3, 5)]
        public int CVV { get; set; }
    }
}
