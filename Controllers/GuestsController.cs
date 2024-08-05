using Microsoft.AspNetCore.Mvc;
using hendi.Models.Entities;
using hendi.Data;
using System.Linq;

namespace hendi.Controllers
{
    public class GuestsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public GuestsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var guests = _dbContext.Guests.ToList();
            ViewBag.guests = guests;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var guests = _dbContext.Guests.ToList();
            ViewBag.guests = guests;
            return View();
        }

        // POST: Guests/Add
        [HttpPost]
        public IActionResult Add(Guest model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != Request.Form["ConfirmPassword"])
                {
                    ModelState.AddModelError("Password", "Password does not match");
                    ViewBag.guests = _dbContext.Guests.ToList();
                    return View(model);
                }

                var existingGuest = _dbContext.Guests.Any(g => g.Email == model.Email);
                if (existingGuest)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    ViewBag.guests = _dbContext.Guests.ToList();
                    return View(model);
                }

                string address = model.Address;
                if (address != null && (address.Length > 15 || address.Length <= 3))
                {
                    ModelState.AddModelError("Address", "Address should be between 3 and 15 characters");
                    ViewBag.guests = _dbContext.Guests.ToList();
                    return View(model);
                }

                _dbContext.Guests.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Add));
            }

            ViewBag.guests = _dbContext.Guests.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var guest = _dbContext.Guests.Find(id);
            if (guest != null)
            {
                _dbContext.Guests.Remove(guest);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Add));
        }
    }
}
