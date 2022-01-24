using System;
using System.ComponentModel.DataAnnotations;
using TokenGenerator.Utils;

namespace TokenGenerator.DTL.v1
{
    public class CardPostDto
    {
        [Required]
        public int? CostumerId { get; set; }
        [Required]
        [NumberLenght(16, 16)]
        public long? CardNumber { get; set; }
        [Required]
        [NumberLenght(3, 5)]
        public int? CVV { get; set; }
    }

    public class CardPostResponseDto
    {
        public DateTime RegistrationDate {  get; set; }
        public long Token { get; set; }
        public int CardId { get; set; }
    }
}
