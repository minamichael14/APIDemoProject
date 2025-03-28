using System.ComponentModel.DataAnnotations;

namespace APIDay1.Validators
{
    public class CustomLocation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string loc = value.ToString();
            if(loc == null)
            {
                return new ValidationResult($"This vlaue is required");
            }
            if (loc != "EG" || loc != "USA")
            {
                return new ValidationResult($"This location vlaue must be 'EG' or 'USA' ");
            }

            return ValidationResult.Success;
        } 
    }
}
