
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Business.Abstract
{
    public interface ITerminationService
    {
        Task<Result<string>> InitiateTermination(int employeeId, string reason);
        Task<Result<string>> ApproveHRCalculation(int employeeId);
        Task<Result<string>> EmployeeApprove(int employeeId);
        Task<Result<List<TerminationDetailDto>>> GetPendingAccountingPayments();
        Task<Result<decimal>> FinalizeTermination(int employeeId, TerminationType type);
        Task<Result<string>> SubmitResignation(int employeeId, string reason, int departmentId);
        Task<Result<string>> ApproveResignation(int employeeId);
        Task<Result<TerminationDetailDto>> GetTerminationDetailForEmployee(int employeeId);
        Task<Result<List<TerminationDetailDto>>> GetAll();
        Task<decimal> CalculateSeveranceAndNotice(Employee employee);
    }
}