using D28_Project4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D25_Project4.DataAccess
{
    public class EFProductDal : IProductDal
    {
        List<Product> _products;
        public EFProductDal()
        {
            _products = new List<Product>
            {
                new Product{ ProductId=1, ProductName = "Acer ef Bilgisayar", QuantityPerUnit = "32 Gb Ram", UnitPrice = 10000, UnitsInStock = 2 },
                new Product{ ProductId=2, ProductName = "Asus ef Bilgisayar", QuantityPerUnit = "32 Gb Ram", UnitPrice = 10000, UnitsInStock = 1 },
                new Product{ ProductId=3, ProductName = "Hp ef Bilgisayar", QuantityPerUnit = "32 Gb Ram", UnitPrice = 10000, UnitsInStock = 0 },
                new Product{ ProductId=4, ProductName = "Mac ef Bilgisayar", QuantityPerUnit = "32 Gb Ram", UnitPrice = 10000, UnitsInStock = 3 },
                new Product{ ProductId=5, ProductName = "Dell ef Bilgisayar", QuantityPerUnit = "32 Gb Ram", UnitPrice = 10000, UnitsInStock = 10 }
            };
        }
        public void Add(Product product)
        {
            Console.WriteLine("EF ile Eklendi");
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
