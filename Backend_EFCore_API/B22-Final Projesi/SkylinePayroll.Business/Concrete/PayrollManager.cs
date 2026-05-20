using Microsoft.AspNetCore.Http;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Constans;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities;
using SkylinePayroll.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Business.Concrete
{
    public class PayrollManager : IPayrollService
    {
        private readonly IPayrollDal _payrollDal;
        private readonly IEmployeeDal _employeeDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        public PayrollManager(IPayrollDal payrollDal, IEmployeeDal employeeDal, IHttpContextAccessor httpContextAccessor, IMailService mailService, IUserService userService, INotificationService notificationService)
        {
            _payrollDal = payrollDal;
            _employeeDal = employeeDal;
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
            _userService = userService;
            _notificationService = notificationService;
        }

        public async Task<Result<DashboardSummaryDto>> GetDashboardSummary()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var activeEmployees = await _employeeDal.GetListAsync(e => e.Status == EmployeeStatus.Active);
            var currentPayrolls = await _payrollDal.GetListAsync(p =>
                p.Period.Month == currentMonth && p.Period.Year == currentYear);

            var dto = new DashboardSummaryDto
            {
                TotalActiveEmployees = activeEmployees.Count,
                PendingPayrolls = activeEmployees.Count - currentPayrolls.Count,
                TotalMonthlyCost = currentPayrolls.Sum(p => p.NetSalary),
                LastProcessedMonth = DateTime.Now.ToString("MMMM yyyy")
            };

            return Result<DashboardSummaryDto>.Ok(dto);
        }

        public async Task<Result<List<Payroll>>> GetEmployeePayrollHistory(int employeeId)
        {
            var history = await _payrollDal.GetListAsync(p => p.EmployeeId == employeeId);
            return Result<List<Payroll>>.Ok(history, "Payroll history retrieved successfully.");
        }

        public async Task<Result<string>> RunMonthlyPayroll()
        {
            var userDepartment = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type == "Department")?.Value;

            if (userDepartment != "Accounting")
            {
                return Result<string>.Error("Access Denied: Only employees in Accounting department can perform this operation.");
            }

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            var details = await _employeeDal.GetEmployeeDetails();
            var activeEmployees = details.Where(e => e.Status == EmployeeStatus.Active).ToList();

            if (!activeEmployees.Any())
                return Result<string>.Error("No active employees found.");

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    int processedCount = 0;
                    bool isLate = DateTime.Now.Day > 1;
                    string lateMessage = isLate ? "Gecikme için üzgünüz" : "";

                    foreach (var emp in activeEmployees)
                    {
                        var existingPayroll = await _payrollDal.GetAsync(p =>
                            p.EmployeeId == emp.Id &&
                            p.Period.Month == currentMonth &&
                            p.Period.Year == currentYear);

                        if (existingPayroll != null)
                        {
                            continue;
                        }

                        var payroll = new Payroll
                        {
                            EmployeeId = emp.Id,
                            Period = new DateTime(currentYear, currentMonth, 1),
                            GrossSalary = emp.Salary,
                            Bonus = 0,
                            TaxAmount = emp.Salary * 0.15m,
                            NetSalary = (emp.Salary + 0) - (emp.Salary * 0.15m),
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };

                        await _payrollDal.AddAsync(payroll);
                        processedCount++;
                        string msg = $"{DateTime.Now:MMMM yyyy} dönemi bordronuz yayınlanmıştır.{lateMessage}";
                        string mailBody = $"Dear {emp.FirstName}, Your payroll for {DateTime.Now:MMMM yyyy} period ({payroll.NetSalary:N2} TL) has been successfully processed.{lateMessage}";
                        var user = await _userService.GetByIdAsync(emp.UserId);
                        if (user != null)
                        {
                            _mailService.SendEmail(user.Email, $"Payroll Notification - {DateTime.Now:MMMM yyyy}", mailBody);
                        }
                        await _notificationService.SendNotificationAsync(message: msg, type: NotificationTypes.Payroll, targetActionId: payroll.Id, targetUserId: emp.UserId);
                    }

                    if (processedCount == 0)
                    {
                        return Result<string>.Error("Tüm aktif personellerin bu ayki ödemeleri zaten yapılmış.");
                    }

                    scope.Complete();
                    return Result<string>.Ok(null, $"{processedCount} records processed successfully.");
                }
                catch (Exception ex)
                {
                    return Result<string>.Error("An error occurred during payroll: " + ex.Message);
                }
            }
        }
    }
}
