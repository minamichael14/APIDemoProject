using System.ComponentModel.DataAnnotations;
using APIDay1.Data;

namespace APIDay1.Validators
{
    public class UniqueName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (AppDbContext) validationContext.GetService(typeof(AppDbContext));
            var nameExist = context.Departments.Any(x => x.Name == value);
            if (nameExist)
            {
                return new ValidationResult($"This name {value} alraedy exist");
            }
            return ValidationResult.Success;
        }
    }

}
