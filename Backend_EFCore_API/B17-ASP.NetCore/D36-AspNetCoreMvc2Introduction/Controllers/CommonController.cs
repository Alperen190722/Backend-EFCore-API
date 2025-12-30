using Microsoft.AspNetCore.Mvc;

namespace D36_AspNetCoreMvc2Introduction.Controllers
{
    public class CommonController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
