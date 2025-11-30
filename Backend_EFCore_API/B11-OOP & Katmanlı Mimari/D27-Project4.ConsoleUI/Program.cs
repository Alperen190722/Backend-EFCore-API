using D25_Project4.DataAccess;
using D26_Project4.Bussines;
using D28_Project4.Entities;

namespace D27_Project4.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new XProductDal());

            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }
            try
            {
                productManager.Add(new Product { ProductId = 10, ProductName = "Laptop", QuantityPerUnit = "4 ayaklı masa", UnitPrice = 1000, UnitsInStock = 10 });
            }
            catch (DuplicateProductException exception)
            {
                Console.WriteLine(exception.Message);
            } 
        }
    }
}
