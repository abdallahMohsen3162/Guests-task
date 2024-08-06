using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using hendi.Data;
using hendi.Models.Entities;

namespace hendi.Models.Validation
{
    public class UniqueEmailForCreate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                var dbContext = validationContext.GetService<ApplicationDbContext>();
                if (dbContext != null)
                {
                    var instance = validationContext.ObjectInstance as Guest;
                    if (instance != null)
                    {
                        var existingGuest = dbContext.Guests.Any(g => g.Email == email && g.Id != instance.Id);
                        if (existingGuest)
                        {
                            return new ValidationResult("Email already exists");
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}



