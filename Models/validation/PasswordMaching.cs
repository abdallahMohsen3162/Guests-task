using System;
using System.ComponentModel.DataAnnotations;

namespace hendi.Models.Validation
{
    public class ComparePasswordsAttribute : ValidationAttribute
    {
        private readonly string _passwordPropertyName;

        public ComparePasswordsAttribute(string passwordPropertyName)
        {
            _passwordPropertyName = passwordPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty(_passwordPropertyName);
            if (passwordProperty == null)
            {
                return new ValidationResult($"Property '{_passwordPropertyName}' is not found.");
            }

            var passwordValue = passwordProperty.GetValue(validationContext.ObjectInstance, null) as string;
            var confirmPasswordValue = value as string;

            if (passwordValue != confirmPasswordValue)
            {
                return new ValidationResult("Passwords do not match.");
            }

            return ValidationResult.Success;
        }
    }
}
