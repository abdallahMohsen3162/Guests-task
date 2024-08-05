using System;
using System.ComponentModel.DataAnnotations;

namespace hendi.Models.Validation
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateOnly birthdate)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                int age = today.Year - birthdate.Year;

                // Adjust age if the birthdate has not yet occurred this year
                if (birthdate > today.AddYears(-age))
                {
                    age--;
                }

                if (age >= _minimumAge)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("The minimum age is " + _minimumAge);
            }

            return new ValidationResult("Wrong format");
        }
    }
}
