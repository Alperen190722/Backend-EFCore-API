namespace D14_Classes2
{
    internal class Program
    {
        //instance = örnek
        static void Main(string[] args)
        {
            Product product1 = new Product() {ProductName = "Laptop", UnitPrice = 5000, UnitsInStock = 5 };
            

            ProductManager productManager = new ProductManager();
            productManager.Add(product1);
        }
    }
}
