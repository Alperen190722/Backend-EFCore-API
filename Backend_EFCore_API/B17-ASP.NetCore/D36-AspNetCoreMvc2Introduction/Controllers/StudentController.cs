using D36_AspNetCoreMvc2Introduction.Entities;
using D36_AspNetCoreMvc2Introduction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace D36_AspNetCoreMvc2Introduction.Controllers
{
    [Route("student/index")]
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;
        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        //[Authorize]
        public async Task<IActionResult> Index() 
        { 
            return View(await _context.Students.ToListAsync());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student); // Hata varsa sayfaya geri dön
            }
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Kayıttan sonra listeye dön
        }
    }
}
