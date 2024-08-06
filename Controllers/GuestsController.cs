using Microsoft.AspNetCore.Mvc;
using hendi.Models.Entities;
using hendi.Models.ViewModels;
using hendi.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

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
            var model = new GuestViewModel
            {
                Guests = guests
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new GuestViewModel
            {
                Guests = _dbContext.Guests.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GuestViewModel model)
        {
            

            foreach(var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                ModelState.AddModelError("", error.ErrorMessage);
                Console.WriteLine(error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                _dbContext.Guests.Add(model.Guest);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Guests = _dbContext.Guests.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var guest = await _dbContext.Guests.FindAsync(id);
            if (guest != null)
            {
                _dbContext.Guests.Remove(guest);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var guest = await _dbContext.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            var model = new GuestViewModel
            {
                Guest = guest,
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GuestViewModel model)
        {
            if (id != model.Guest.Id)
            {
                return BadRequest();
            }

            var existingGuest = await _dbContext.Guests.FindAsync(id);
            if (existingGuest == null)
            {
                return NotFound();
            }
            string existingPassword = existingGuest.Password;
            if (string.IsNullOrEmpty(model.Guest.Password) && string.IsNullOrEmpty(model.Guest.confirmPassword))
            {
                ModelState.Remove("Guest.Password");
                ModelState.Remove("Guest.confirmPassword");
            }
            if (!ModelState.IsValid)
            {
                model.Guests = await _dbContext.Guests.ToListAsync();
                return View(model);
            }
            existingGuest.Name = model.Guest.Name;
            existingGuest.Birthdate = model.Guest.Birthdate;
            existingGuest.Phone = model.Guest.Phone;
            existingGuest.Email = model.Guest.Email;
            existingGuest.DateOfAppintment = model.Guest.DateOfAppintment;
            existingGuest.Address = model.Guest.Address;
            if (!string.IsNullOrEmpty(model.Guest.Password) && model.Guest.Password != existingPassword)
            {
                existingGuest.Password = model.Guest.Password;
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
