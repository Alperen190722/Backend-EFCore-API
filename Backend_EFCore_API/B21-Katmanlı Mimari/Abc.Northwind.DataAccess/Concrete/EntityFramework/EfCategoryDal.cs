using Abc.Core.EntityFramework;
using Abc.Northwind.DataAccess.Abstract;
using Abc.Northwind.Entities.Concrete;

namespace Abc.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {

    }
}
