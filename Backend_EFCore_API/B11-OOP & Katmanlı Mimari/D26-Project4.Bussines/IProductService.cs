using D28_Project4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D26_Project4.Bussines
{
    public interface IProductService
    {
        List<Product> GetAll();
        void Add(Product product);
    }
}
