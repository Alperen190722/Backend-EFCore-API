using Abc.Northwind.Entities.Concrete;

namespace D40_Abc.Northwind.MvcWebUI.Models
{
    public class ProductAddViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; internal set; }
    }
}
