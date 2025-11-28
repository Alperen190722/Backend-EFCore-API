using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D16_Project3
{
    internal class CustomerManager
    {
        public CustomerManager()
        {
            customers = new List<Customer>()
            {
                new Customer{Id = 1, FirstName = "Alperen", LastName = "Pişkin", City = "İstanbul", Email = "alperen@"},
                new Customer{Id = 2, FirstName = "Furkan", LastName = "Sülek", City = "Konya", Email = "furkan@"},
                new Customer{Id = 3, FirstName = "Semih", LastName = "Tecer", City = "Sivas", Email = "semih@"},
                new Customer{Id = 4, FirstName = "Ali", LastName = "Koçkan", City = "Tekirdağ", Email = "ali@"},
                new Customer{Id = 5, FirstName = "Belinay", LastName = "Pişkin", City = "Bursa", Email = "belinay@"},
            };
        }

        List<Customer> customers;
        public List<Customer> GetAll() 
        {
            //Veritabanına bağlan


            return customers;
        }

        public void Add(Customer customer) 
        {
            customers.Add(customer);
        }
    }
}
