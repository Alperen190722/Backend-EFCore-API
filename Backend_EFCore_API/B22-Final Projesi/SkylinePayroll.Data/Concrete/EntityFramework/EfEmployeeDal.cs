using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Core.Concrete.EntityFramework;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Data.Abstract;

namespace SkylinePayroll.Data.Concrete.EntityFramework
{
    public class EfEmployeeDal : EfEntityRepositoryBase<Employee, SkylineContext>, IEmployeeDal
    {
        public EfEmployeeDal(SkylineContext context) : base(context)
        {
        }

        public async Task<List<Employee>> GetEmployeeDetails()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .ToListAsync();
        }
    }
}