using System;

namespace TokenGenerator.Models
{
    public class Token
    {
        public Token()
        {
            RegistrationDate = DateTime.UtcNow;
        }

        public Token(long content) : this()
        {
            Content = content;
        }
        public int Id { get; set; }
        public long Content { get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}