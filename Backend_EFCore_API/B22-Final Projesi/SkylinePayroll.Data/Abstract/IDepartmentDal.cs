using SkylinePayroll.Core.Abstract;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Data.Abstract
{
    public interface IDepartmentDal : IEntityRepository<Department>
    {
        Task<List<DepartmentListDto>> GetDepartmentDetails();
    }
}
