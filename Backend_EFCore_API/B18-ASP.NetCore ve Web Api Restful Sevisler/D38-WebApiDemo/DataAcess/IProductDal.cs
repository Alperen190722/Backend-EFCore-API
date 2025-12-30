using D38_WebApiDemo.Entities;
using D38_WebApiDemo.Models;

namespace D38_WebApiDemo.DataAcess
{
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductModel> GetProductWithDetails();
    }
}
