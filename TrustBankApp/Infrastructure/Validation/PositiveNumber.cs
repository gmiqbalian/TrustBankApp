﻿using System.ComponentModel.DataAnnotations;

namespace TrustBankApp.Infrastructure.Validation
{
    public class PositiveNumber: ValidationAttribute
    {
        public PositiveNumber()
        {
            ErrorMessage = "Number must be positive and between 10 to 100000";
        }
        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            int number = int.Parse(value.ToString());

            if (number > 10 || number <= 100000)
                return ValidationResult.Success;
            return
                new ValidationResult(ErrorMessage);
        }

    }
}
