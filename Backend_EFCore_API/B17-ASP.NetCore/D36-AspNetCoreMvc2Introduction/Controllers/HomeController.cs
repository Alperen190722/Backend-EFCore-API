using D36_AspNetCoreMvc2Introduction.Entities;
using D36_AspNetCoreMvc2Introduction.Filters;
using D36_AspNetCoreMvc2Introduction.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Security;

namespace D36_AspNetCoreMvc2Introduction.Controllers
{
    [Route ("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HandleException(ViewName = "DivedByZeroError",ExceptionType = typeof(DivideByZeroException))]
        [HandleException(ViewName = "Error", ExceptionType = typeof(SecurityException))]
        [HttpGet("Index2")]
        public ViewResult Index2() 
        {
            throw new SecurityException();
            return View();
        }
        [HttpGet("Index3")]
        public IActionResult Index3()
        {
            List<Employee> employees = new List<Employee>()
            {
               new Employee() { Id = 1, FirstName = "Alperen", LastName = "Pişkin", CityId = 34 },
               new Employee() { Id = 2, FirstName = "Furkan", LastName = "Sülek", CityId = 16 },
               new Employee() { Id = 3, FirstName = "Semih", LastName = "Tecer", CityId = 08 }
            };

            List<string> cities = new List<string> {"İstanbul", "Ankara"};

            var model = new EmployeeListViewModel
            {
                Employees = employees,
                Cities = cities
            };
            return Ok(model);
        }
        [HttpGet("Index4")]
        public IActionResult Index4() 
        {
            return View();
        }
        [HttpGet("Index5")]
        public IActionResult Index5() 
        {
            return NotFound();
        }

        [HttpGet("Index6")]
        public RedirectResult Index6() 
        {
            return Redirect("/Home/Index2");
        }

        [HttpGet("Index7")]
        public IActionResult Index7()
        {
            return RedirectToAction("Index3");
        }
        [HttpGet("Index8")]
        public IActionResult Index8() 
        {
            return RedirectToRoute("default");
        }

        [HttpGet("Index9")]
        public JsonResult Index9() 
        {
            List<Employee> employees = new List<Employee>()
            {
               new Employee() { Id = 1, FirstName = "Alperen", LastName = "Pişkin", CityId = 34 },
               new Employee() { Id = 2, FirstName = "Furkan", LastName = "Sülek", CityId = 16 },
               new Employee() { Id = 3, FirstName = "Semih", LastName = "Tecer", CityId = 08 }
            };
            return Json(employees);
        }
    }
}

//http://alperenpiskinorg.com/home/index
//HomeController h = new HomeController();
//h.Index();
