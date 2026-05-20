using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Business.Abstract
{
    public interface IEmployeeService
    {
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task DeleteAsync(int id);
        Task UpdateAsync(EmployeeUpdateDto employeeUpdateDto);
        Task<List<EmployeeListDto>> SearchEmployeesAsync(EmployeeSearchDto searchDto);
        Task<Employee> GetByUserIdAsync(int userId);
        Task UpdateStatusAsync(int id, EmployeeStatus status);
        Task<List<EmployeeListDto>> GetFilteredManagementListAsync(int currentUserId);

    }
}
