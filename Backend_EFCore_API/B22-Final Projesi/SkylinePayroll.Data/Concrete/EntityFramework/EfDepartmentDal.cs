using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Core.Concrete.EntityFramework;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Data.Abstract;

namespace SkylinePayroll.Data.Concrete.EntityFramework
{
    public class EfDepartmentDal : EfEntityRepositoryBase<Department, SkylineContext>, IDepartmentDal
    {
        public EfDepartmentDal(SkylineContext context) : base(context)
        {
        }

        public async Task<List<DepartmentListDto>> GetDepartmentDetails()
        {
            return await _context.Departments
                .Include(d => d.Roles)
                .Select(d => new DepartmentListDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    IsActive = d.IsActive,
                    CreatedDate = d.CreatedDate,
                    RoleNames = d.Roles.Select(r => r.Name).ToList()
                }).ToListAsync();
        }
    }
}