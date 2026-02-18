using Abc.Northwind.Entities.Concrete;

namespace Abc.Northwind.Bussines.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}
