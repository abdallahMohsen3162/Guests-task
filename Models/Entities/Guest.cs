using hendi.Models.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace hendi.Models.Entities
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinimumAge(18)]
        public DateOnly Birthdate { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateOnly DateOfAppintment { get; set; }

        public string? Address { get; set; }
    }
}
