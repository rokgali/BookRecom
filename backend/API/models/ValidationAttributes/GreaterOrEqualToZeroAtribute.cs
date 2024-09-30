using System.ComponentModel.DataAnnotations;

namespace backend.models.gemini.validationAttributes 
{
    public class GreaterOrEqualToZero : ValidationAttribute
    {
        public GreaterOrEqualToZero()
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null)
                return ValidationResult.Success;

            if(value is int intValue)
            {
                return intValue >= 0 ? ValidationResult.Success : new ValidationResult($"The field {validationContext.MemberName} must be greater or equal to zero");
            }

            return new ValidationResult($"The field {validationContext.MemberName} must be an integer");
        }
    }
}