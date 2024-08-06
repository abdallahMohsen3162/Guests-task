using hendi.Models.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace hendi.Models.Entities
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        [MinimumAge(18, ErrorMessage = "You must be at least 18 years old")]
        public DateOnly Birthdate { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of Appointment is required")]
        public DateOnly DateOfAppintment { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Address should be between 3 and 15 characters")]
        public string? Address { get; set; }
    }
}
