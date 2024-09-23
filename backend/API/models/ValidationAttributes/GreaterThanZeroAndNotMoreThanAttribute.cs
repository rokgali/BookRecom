using System.ComponentModel.DataAnnotations;

namespace backend.models.gemini.validationAttributes 
{
    public class GreaterThanZeroAndNotMoreThanAttribute : ValidationAttribute
    {
        private readonly int _notMoreThan;

        public GreaterThanZeroAndNotMoreThanAttribute(int notMoreThan)
        {
            _notMoreThan = notMoreThan;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return ValidationResult.Success;
            }

            if(value is int intValue)
            {
                return intValue > 0 && intValue <= _notMoreThan ? ValidationResult.Success : 
                       new ValidationResult($"The field {validationContext.MemberName} must be greater than 0 and less or equal to {_notMoreThan}");
            }

            return new ValidationResult($"The field {validationContext.MemberName} must be an integer");
        }
    }
}