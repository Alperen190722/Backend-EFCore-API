using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Core.Concrete.EntityFramework;
using SkylinePayroll.Core.Entities;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Data.Abstract;
using SkylinePayroll.Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SkylinePayroll.Data.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, SkylineContext>, IUserDal
    {
        public EfUserDal(SkylineContext context): base(context)
        {
            
        }
        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            var result = from operationClaim in _context.OperationClaims 
                         join userOperationClaim in _context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim
                         {
                             Id = operationClaim.Id,
                             Name = operationClaim.Name
                         };

            return await result.ToListAsync();
        }

        public async Task<IUserDetailed> GetUserDetails(int userId)
        {
            var result = from user in _context.Users
                         join employee in _context.Employees on user.Id equals employee.UserId
                         join dept in _context.Departments on employee.DepartmentId equals dept.Id
                         join role in _context.Roles on employee.RoleId equals role.Id
                         where user.Id == userId
                         select new UserDetailDto
                         {
                             Id = user.Id,
                             EmployeeId = employee.Id,
                             Email = user.Email,
                             FirstName = employee.FirstName,
                             LastName = employee.LastName,
                             DepartmentId = employee.DepartmentId,
                             DepartmentName = dept.Name,
                             RoleName = role.Name,
                             HierarchyLevel = role.HierarchyLevel
                         };

            return await result.FirstOrDefaultAsync();
        }
    }
}
