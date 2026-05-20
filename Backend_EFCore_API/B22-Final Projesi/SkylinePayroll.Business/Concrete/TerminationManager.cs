using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Constans;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities;
using SkylinePayroll.Data.Abstract;
using SkylinePayroll.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Business.Concrete
{
    public class TerminationManager : ITerminationService
    {
        private readonly ITerminationDal _terminationDal;
        private readonly IEmployeeDal _employeeDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;
        private readonly IDepartmentDal _departmentDal;
        private readonly SkylineContext _context;

        public TerminationManager(
            ITerminationDal terminationDal,
            IEmployeeDal employeeDal,
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IDepartmentDal departmentDal,
            SkylineContext context)
        {
            _terminationDal = terminationDal;
            _employeeDal = employeeDal;
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
            _departmentDal = departmentDal;
            _context = context;
        }

        public async Task<Result<string>> InitiateTermination(int employeeId, string reason)
        {
            var employee = await _employeeDal.GetAsync(e => e.Id == employeeId);
            if (employee == null) return Result<string>.Error("Personel bulunamadı.");

            if (employee.Status != EmployeeStatus.Active)
            {
                return Result<string>.Error("Bu personel zaten bir fesih sürecinde veya pasif durumda.");
            }

            var existingActiveTermination = await _terminationDal.GetAsync(t => t.EmployeeId == employeeId && t.Status != TerminationStatus.AccountingPaid);
            if (existingActiveTermination != null)
            {
                return Result<string>.Error("Bu personel için hali hazırda devam eden bir fesih dosyası var.");
            }

            employee.Status = EmployeeStatus.PendingTermination;

            var termination = new Termination
            {
                EmployeeId = employeeId,
                NoticeDate = DateTime.Now,
                Reason = reason,
                Type = TerminationType.Dismissal,
                Status = TerminationStatus.ManagerApproved,
                CreatedDate = DateTime.Now
            };

            await _terminationDal.AddAsync(termination);
            await _employeeDal.UpdateAsync(employee);

            var hrDept = await _departmentDal.GetAsync(d => d.Name.Trim().ToLower() == DepartmentNames.HR.Trim().ToLower());
            if (hrDept != null)
            {
                await _notificationService.SendNotificationAsync(
                    $"{employee.FirstName} {employee.LastName} için fesih süreci başlatıldı. Hesaplama bekliyor.",
                    NotificationTypes.Termination,
                    employee.Id,
                    null,
                    hrDept.Id
                );
            }
            return Result<string>.Ok(null, "Fesih süreci başarıyla başlatıldı.");
        }

        public async Task<Result<string>> ApproveHRCalculation(int employeeId)
        {
            var termination = await _terminationDal.GetAsync(t => t.EmployeeId == employeeId && (int)t.Status == 1 && t.IsDeleted == false);
            var employee = await _employeeDal.GetAsync(e => e.Id == employeeId);

            if (termination == null || employee == null) return Result<string>.Error("Pending HR process not found.");

            termination.CalculatedAmount = await CalculateSeveranceAndNotice(employee);
            termination.Status = TerminationStatus.WaitingForEmployee;
            termination.HrApprovalDate = DateTime.Now;

            await _terminationDal.UpdateAsync(termination);

            await _notificationService.ClearNotificationAsync(employeeId, NotificationTypes.Termination);

            await _notificationService.SendNotificationAsync(
                "Fesih hesaplamalarınız hazırlandı, lütfen onaylayınız.",
                NotificationTypes.Termination,
                employeeId,
                employee.UserId,
                null
            );

            return Result<string>.Ok(null, "Sent to employee for approval.");
        }

        public async Task<Result<string>> EmployeeApprove(int employeeId)
        {
            var termination = await _terminationDal.GetAsync(t => t.EmployeeId == employeeId && (int)t.Status == 2 && t.IsDeleted == false);
            if (termination == null) return Result<string>.Error("No pending employee approval found.");

            termination.Status = TerminationStatus.HRProcessed;
            await _terminationDal.UpdateAsync(termination);

            await _notificationService.ClearNotificationAsync(employeeId, NotificationTypes.Termination);

            var accountingDept = await _departmentDal.GetAsync(d => d.Name.ToLower() == "accounting" || d.Name.ToLower() == "muhasebe");
            if (accountingDept != null)
            {
                await _notificationService.SendNotificationAsync(
                    "Yeni bir ödeme emri onaylandı.",
                    NotificationTypes.Compensation,
                    employeeId,
                    null,
                    accountingDept.Id
                );
            }
            return Result<string>.Ok(null, "Approved by employee.");
        }

        public async Task<Result<TerminationDetailDto>> GetTerminationDetailForEmployee(int employeeId)
        {
            var employee = await _employeeDal.GetAsync(e => e.Id == employeeId);

            var termination = await _context.Terminations
                .Include(t => t.Employee)
                .Where(t => t.EmployeeId == employeeId && (int)t.Status != 4 && t.IsDeleted == false)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            if (termination != null)
            {
                return Result<TerminationDetailDto>.Ok(new TerminationDetailDto
                {
                    Id = termination.Id,
                    EmployeeId = termination.EmployeeId,
                    Reason = termination.Reason,
                    TotalAmount = termination.CalculatedAmount,
                    Status = ((int)termination.Status).ToString(),
                    Iban = termination.Employee?.Iban ?? "IBAN Tanımlı Değil"
                });
            }
            return Result<TerminationDetailDto>.Ok(new TerminationDetailDto { Status = "-1" });
        }

        public async Task<Result<List<TerminationDetailDto>>> GetPendingAccountingPayments()
        {
            var userDepartment = _httpContextAccessor.HttpContext?.User.FindFirst("Department")?.Value;
            if (userDepartment != "Accounting" && userDepartment != "Accountant")
                return Result<List<TerminationDetailDto>>.Error("Unauthorized.");

            var pendingList = await _context.Terminations
                .Include(x => x.Employee)
                .Where(x => x.Status == TerminationStatus.HRProcessed)
                .Select(t => new TerminationDetailDto
                {
                    Id = t.Id,
                    EmployeeId = t.EmployeeId,
                    EmployeeName = t.Employee.FirstName + " " + t.Employee.LastName,
                    TotalAmount = t.CalculatedAmount,
                    Iban = t.Employee.Iban,
                    Reason = t.Reason,
                    Status = t.Status.ToString(),
                    HrApprovalDate = t.HrApprovalDate
                }).ToListAsync();

            await _notificationService.ClearNotificationAsync(0, NotificationTypes.Compensation);
            return Result<List<TerminationDetailDto>>.Ok(pendingList);
        }

        public async Task<Result<decimal>> FinalizeTermination(int employeeId, TerminationType type)
        {
            var userRole = _httpContextAccessor.HttpContext?.User.FindFirst("Department")?.Value;
            if (userRole != "Accounting" && userRole != "Accountant")
                return Result<decimal>.Error("Unauthorized.");

            var termination = await _terminationDal.GetAsync(t => t.EmployeeId == employeeId && (int)t.Status == 3);
            var employee = await _employeeDal.GetAsync(e => e.Id == employeeId);

            if (termination == null || employee == null) return Result<decimal>.Error("Not found.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == employee.UserId);
            if (user != null)
            {
                user.Status = false;
            }

            termination.Status = TerminationStatus.AccountingPaid;
            termination.TerminationDate = DateTime.Now;
            termination.Type = type;
            employee.Status = EmployeeStatus.Passive;

            await _terminationDal.UpdateAsync(termination);
            await _employeeDal.UpdateAsync(employee);

            await _context.SaveChangesAsync();

            await _notificationService.ClearNotificationAsync(employeeId, NotificationTypes.Compensation);

            var managementDept = await _departmentDal.GetAsync(d => d.Name.ToLower() == "management" || d.Name.ToLower() == "yönetim");
            if (managementDept != null)
            {
                await _notificationService.SendNotificationAsync(
                    $"{employee.FirstName} {employee.LastName} fesih süreci tamamlandı ve ödemesi yapıldı.",
                    NotificationTypes.Compensation,
                    employee.Id,
                    null,
                    managementDept.Id
                );
            }

            return Result<decimal>.Ok(termination.CalculatedAmount, "Process finalized.");
        }

        public async Task<Result<string>> SubmitResignation(int employeeId, string reason, int departmentId)
        {
            var employee = await _employeeDal.GetAsync(e => e.Id == employeeId);
            if (employee == null) return Result<string>.Error("Personel bulunamadı!");

            if (employee.Status != EmployeeStatus.Active)
            {
                return Result<string>.Error("Mevcut iş durumunuz nedeniyle istifa talebi oluşturulamaz.");
            }

            var mgmtDept = await _departmentDal.GetAsync(d => d.Name.ToLower() == "management" || d.Name.ToLower() == "yönetim");

            if (mgmtDept != null && employee.DepartmentId == mgmtDept.Id)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == employee.UserId);
                if (user != null) user.Status = false;

                employee.Status = EmployeeStatus.Passive;

                var directTermination = new Termination
                {
                    EmployeeId = employeeId,
                    Reason = reason,
                    Type = TerminationType.Resignation,
                    Status = TerminationStatus.AccountingPaid,
                    TerminationDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                };

                await _terminationDal.AddAsync(directTermination);
                await _employeeDal.UpdateAsync(employee);
                await _context.SaveChangesAsync();

                return Result<string>.Ok("Yönetim istifası başarıyla tamamlandı ve hesap pasifleştirildi.");
            }

            employee.Status = EmployeeStatus.PendingResignation;
            await _employeeDal.UpdateAsync(employee);

            var termination = new Termination
            {
                EmployeeId = employeeId,
                Reason = reason,
                Type = TerminationType.Resignation,
                Status = TerminationStatus.ResignationSubmitted,
                CreatedDate = DateTime.Now
            };
            await _terminationDal.AddAsync(termination);

            if (mgmtDept != null && employee.DepartmentId != mgmtDept.Id)
            {
                await _notificationService.SendNotificationAsync(
                    $"{employee.FirstName} {employee.LastName} istifasını sundu.",
                    NotificationTypes.Resignation,
                    employeeId,
                    null,
                    mgmtDept.Id
                );
            }

            return Result<string>.Ok("İstifa sunuldu.");
        }

        public async Task<Result<string>> ApproveResignation(int employeeId)
        {
            var resignation = await _terminationDal.GetAsync(t => t.EmployeeId == employeeId &&
                                                                 t.Status == TerminationStatus.ResignationSubmitted);
            var employee = await _employeeDal.GetAsync(e => e.Id == employeeId);

            if (resignation == null || employee == null)
                return Result<string>.Error("Onaylanacak aktif bir istifa süreci bulunamadı.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == employee.UserId);
            if (user != null)
            {
                user.Status = false;
                _context.Entry(user).State = EntityState.Modified;
            }

            resignation.Status = TerminationStatus.AccountingPaid;
            resignation.TerminationDate = DateTime.Now;
            employee.Status = EmployeeStatus.Passive;

            await _terminationDal.UpdateAsync(resignation);
            await _employeeDal.UpdateAsync(employee);
            await _context.SaveChangesAsync();
            await _notificationService.ClearNotificationAsync(employeeId, "İstifa");

            return Result<string>.Ok("Success", $"{employee.FirstName} {employee.LastName} istifası onaylandı.");
        }

        public async Task<decimal> CalculateSeveranceAndNotice(Employee employee)
        {
            int totalDays = (DateTime.Now - employee.HireDate).Days;
            decimal severance = totalDays >= 365 ? (totalDays / 365.0m) * employee.Salary : 0;
            decimal notice = totalDays >= 180 ? employee.Salary * 1.5m : 0;
            return await Task.FromResult(severance + notice);
        }

        public async Task<Result<List<TerminationDetailDto>>> GetAll()
        {
            var list = await _context.Terminations
                .Include(x => x.Employee)
                .Select(t => new TerminationDetailDto
                {
                    Id = t.Id,
                    EmployeeId = t.EmployeeId,
                    EmployeeName = t.Employee.FirstName + " " + t.Employee.LastName,
                    TotalAmount = t.CalculatedAmount,
                    Reason = t.Reason,
                    Status = t.Status.ToString(),
                    HrApprovalDate = t.HrApprovalDate,
                    Iban = t.Employee.Iban
                }).ToListAsync();

            return Result<List<TerminationDetailDto>>.Ok(list);
        }
    }
}