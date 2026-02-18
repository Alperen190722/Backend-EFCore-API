using Abc.Northwind.Entities.Concrete;
using D40_Abc.Northwind.MvcWebUI.Models;
using D40_Abc.Northwind.MvcWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace D40_Abc.Northwind.MvcWebUI.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private ICartSessionService _cartSessionService;
        public CartSummaryViewComponent(ICartSessionService cartSessionService)
        {
            _cartSessionService = cartSessionService;
        }

        public ViewViewComponentResult Invoke()
        {
            var model = new CartSummaryViewModel
            {
                Cart = _cartSessionService.GetCart()
            };
            return View(model);
        }
    }
}
