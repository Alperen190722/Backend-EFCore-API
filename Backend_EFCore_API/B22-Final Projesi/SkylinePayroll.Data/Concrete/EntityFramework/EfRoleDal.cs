using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Core.Concrete.EntityFramework;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Data.Abstract;

namespace SkylinePayroll.Data.Concrete.EntityFramework
{
    public class EfRoleDal : EfEntityRepositoryBase<Role, SkylineContext>, IRoleDal
    {
        public EfRoleDal(SkylineContext context) : base(context)
        {
        }

        public async Task<List<RoleListDto>> GetRoleDetails()
        {
            var result = from role in _context.Roles
                         join dept in _context.Departments on role.DepartmentId equals dept.Id
                         select new RoleListDto
                         {
                             Id = role.Id,
                             Name = role.Name,
                             IsActive = role.IsActive,
                             MinSalary = role.MinSalary,
                             MaxSalary = role.MaxSalary,
                             DepartmentId = role.DepartmentId,
                             DepartmentName = dept.Name
                         };

            return await result.ToListAsync();
        }
    }
}