using System.ComponentModel.DataAnnotations;

namespace TrustBankApp.Infrastructure.Validation
{
    public class Test : ValidationAttribute
    {
        public Test()
        {
            ErrorMessage = "This field is required.";
        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            string input = Convert.ToString(value);

            if (!string.IsNullOrEmpty(input))
                return ValidationResult.Success;
            return 
                new ValidationResult(ErrorMessage);
        }
    }
}
