using Azure.Identity;
using D40_Abc.Northwind.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace D40_Abc.Northwind.MvcWebUI.ViewComponents
{
    public class UserSummaryViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            UserDetailsViewModel model = new UserDetailsViewModel
            {
                UserName = HttpContext.User.Identity.Name
            };
            return View(model);
        }
    }
}
