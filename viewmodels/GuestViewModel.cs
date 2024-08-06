using hendi.Models.Entities;
using hendi.Models.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hendi.Models.ViewModels
{
    public class GuestViewModel
    {
        public Guest Guest { get; set; } = new Guest();

        public IEnumerable<Guest>? Guests { get; set; }
    }
}
