using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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

            var passwordValue = passwordProperty.GetValue(validationContext.ObjectInstance) as string;
            var confirmPasswordValue = value as string;
            if (passwordValue == confirmPasswordValue)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Passwords do not match.");
        }
    }
}
