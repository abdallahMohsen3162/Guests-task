using Microsoft.AspNetCore.Mvc;
using hendi.Models.Entities;
using hendi.Models.ViewModels;
using hendi.Data;
using System.Linq;
using System.Threading.Tasks;

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
            try
            {
                if (model.Guest.Password != Request.Form["ConfirmPassword"])
                {
                    ModelState.AddModelError("Guest.Password", "The password and confirmation password do not match.");
                    model.Guests = _dbContext.Guests.ToList();
                    return View(model);
                }
                if (ModelState.IsValid)
                {
                    _dbContext.Guests.Add(model.Guest);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                model.Guests = _dbContext.Guests.ToList();
                return View(model);

            }catch
            {
                model.Guests = _dbContext.Guests.ToList();
                return View(model);
            }
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

            if (ModelState.IsValid)
            {
                var existingGuest = await _dbContext.Guests.FindAsync(id);
                if (existingGuest == null)
                {
                    return NotFound();
                }
                existingGuest.Name = model.Guest.Name;
                existingGuest.Birthdate = model.Guest.Birthdate;
                existingGuest.Phone = model.Guest.Phone;
                existingGuest.Password = model.Guest.Password;
                existingGuest.DateOfAppintment = model.Guest.DateOfAppintment;
                existingGuest.Address = model.Guest.Address;

                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Guests = _dbContext.Guests.ToList();
            return View(model);
        }


    }
}
