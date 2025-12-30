using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using D36_AspNetCoreMvc2Introduction.Models;
namespace D36_AspNetCoreMvc2Introduction.ViewComponents
{
    public class StudentListViewComponent : ViewComponent
    {
        private SchoolContext _context;

        public StudentListViewComponent(SchoolContext context) 
        {
            _context = context;
        }

        public ViewViewComponentResult Invoke(string filter) 
        {
            filter = HttpContext.Request.Query["filter"];
            return View(new StudentListViewModel 
            { 
                Students = _context.Students.Where(s => s.FirstName.ToLower().Contains(filter)).ToList()
            });
        }
    }
}
