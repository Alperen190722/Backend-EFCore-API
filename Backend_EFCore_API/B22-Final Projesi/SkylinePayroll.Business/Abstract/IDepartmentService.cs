using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Abstract
{
    public interface IDepartmentService
    {
        Task<List<DepartmentListDto>> GetDepartmentDetails();
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task <Department> GetByIdAsync(int id);
        Task DeleteAsync(Department department);
    }
}
