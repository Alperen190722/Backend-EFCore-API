using Abc.Northwind.Bussines.Abstract;
using D40_Abc.Northwind.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace D40_Abc.Northwind.MvcWebUI.Controllers
{
    public class ProductController:Controller
    {
        private IProductService _productServie;

        public ProductController(IProductService productService)
        {
            _productServie = productService;
        }
        public IActionResult Index(int page=1,int category=0)
        {
            int pageSize = 10;
            var products = _productServie.GetByCategory(category);
            ProductListViewModel model = new ProductListViewModel
            {
                Products = products.Skip((page-1)*pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(products.Count/(double)pageSize),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page
            };
            return View(model);
        }

        //public string Session()
        //{
        //    HttpContext.Session.SetString("city","İstanbul");
        //    HttpContext.Session.SetInt32("age", 22);

        //    HttpContext.Session.GetString("city");
        //    HttpContext.Session.GetInt32("age");
        //}
    }
}
