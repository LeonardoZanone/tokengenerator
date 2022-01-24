using System.ComponentModel.DataAnnotations;

namespace TokenGenerator.Utils
{
    public class NumberLenghtAttribute : ValidationAttribute
    {
        private readonly int _minLenght;
        private readonly int _maxLenght;

        public NumberLenghtAttribute(int minLenght, int maxLenght)
        {
            _minLenght = minLenght;
            _maxLenght = maxLenght;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value.ToString().Length < _minLenght || value.ToString().Length > _maxLenght)
            {
                return new ValidationResult($"The field {validationContext.DisplayName} must have a lenght between {_minLenght} and {_maxLenght}.");
            }

            return ValidationResult.Success;
        }
    }
}
