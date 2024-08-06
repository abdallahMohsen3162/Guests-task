using hendi.Data;
using hendi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace hendi.ViewComponents
{
    public class GuestTableViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dbcontext;
        public GuestTableViewComponent(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IViewComponentResult Invoke()
        {

            var model = new GuestViewModel
            {
                Guests = _dbcontext.Guests.ToList()
            };
            return View(model);
        }
    }
}
