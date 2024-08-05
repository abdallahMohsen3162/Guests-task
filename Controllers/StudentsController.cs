using Microsoft.AspNetCore.Mvc;
using hendi.Models.Entities;
using hendi.Data;


namespace hendi.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult index()
        {
            var students = _dbContext.students.ToList();
            var count = _dbContext.students.Count();
            ViewBag.count = count;
            return View(students);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            if (ModelState.IsValid)
            {
                var studentEntity = new Student
                {
                    Id = Guid.NewGuid(),
                    Name = student.Name,
                    Email = student.Email,
                    Phone = student.Phone,
                    Address = student.Address,
                    Age = student.Age
                };

                await _dbContext.students.AddAsync(studentEntity);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(student);
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var student = _dbContext.students.Find(id);
            ViewBag.student = student;
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var student = _dbContext.students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
               _dbContext.Update(student);
               _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var student = await _dbContext.students.FindAsync(id);
            if (student != null)
            {
                _dbContext.students.Remove(student);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("");
        }

        public IActionResult One()
        {

            return View();
        }

        public IActionResult apis()
        {
            var forecast = new
            {
                Date = DateTime.Now,
                TemperatureC = 25,
                Summary = "Warm"
            };
            return Ok(forecast);
        }
    }
}
