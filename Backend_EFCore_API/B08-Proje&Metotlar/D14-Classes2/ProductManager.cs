using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D14_Classes2
{
    //methods
    //reusability
    internal class ProductManager
    {
        public void Add(Product product) 
        {
            Console.WriteLine("Ürün sisteme eklendi : " + product.ProductName);
        }
    }
}
