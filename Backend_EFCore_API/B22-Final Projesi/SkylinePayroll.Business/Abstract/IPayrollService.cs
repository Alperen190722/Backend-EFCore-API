using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Abstract
{
    public interface IPayrollService
    {
        Task<Result<string>> RunMonthlyPayroll();
        Task<Result<List<Payroll>>> GetEmployeePayrollHistory(int employeeId);
        Task<Result<DashboardSummaryDto>> GetDashboardSummary();
    }
}
