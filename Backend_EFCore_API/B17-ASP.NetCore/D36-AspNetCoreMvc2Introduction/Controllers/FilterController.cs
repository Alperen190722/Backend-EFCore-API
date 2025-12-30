using D36_AspNetCoreMvc2Introduction.Filters;
using Microsoft.AspNetCore.Mvc;

namespace D36_AspNetCoreMvc2Introduction.Controllers
{
    public class FilterController : Controller
    {
        [CustomFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}
