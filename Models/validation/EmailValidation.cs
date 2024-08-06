using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using hendi.Data;

namespace hendi.Models.Validation
{
    public class EmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                var dbContext = validationContext.GetService<ApplicationDbContext>();

                if (dbContext != null)
                {
                    
                    var existingGuest = dbContext.Guests.Any(g => g.Email == email);
                    if (existingGuest)
                    {
                        return new ValidationResult("Email already exists.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
