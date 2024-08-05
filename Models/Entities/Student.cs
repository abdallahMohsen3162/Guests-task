using System.ComponentModel.DataAnnotations;
namespace hendi.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, 150)]
        public int Age { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
