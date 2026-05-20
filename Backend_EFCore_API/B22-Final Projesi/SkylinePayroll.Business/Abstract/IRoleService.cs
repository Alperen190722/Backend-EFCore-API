using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Abstract
{
    public interface IRoleService
    {
        Task<List<RoleListDto>> GetRoleDetails();
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task <Role> GetByIdAsync(int id);
        Task DeleteAsync(Role role);
    }
}
