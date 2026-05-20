using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Constans;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Data.Abstract;
using SkylinePayroll.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;
        private readonly IRoleDal _roleDal;
        private readonly IDepartmentDal _departmentDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly INotificationService _notificationService;
        private readonly ITerminationDal _terminationDal;
        private readonly ITerminationService _terminationService;
        private readonly SkylineContext _context;
        public EmployeeManager(IEmployeeDal employeeDal, IRoleDal roleDal, IDepartmentDal departmentDal, IHttpContextAccessor httpContextAccessor, IUserService userService, IMailService mailService, INotificationService notificationService, ITerminationDal terminationDal, ITerminationService terminationService, SkylineContext context)
        {
            _employeeDal = employeeDal;
            _roleDal = roleDal;
            _departmentDal = departmentDal;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mailService = mailService;
            _notificationService = notificationService;
            _terminationDal = terminationDal;
            _terminationService = terminationService;
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            var userDepartment = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type == "Department")?.Value;
            if (userDepartment != "Human Resources" && userDepartment != "Management")
            {
                throw new Exception("Unauthorized: Only Human Resources or Management can add new employees.");
            }
            var role = await _roleDal.GetAsync(r => r.Id == employee.RoleId);
            var department = await _departmentDal.GetAsync(d => d.Id == employee.DepartmentId);

            if (role == null || department == null)
            {
                throw new Exception("Error: Selected Role or Department not found.");
            }
            if (employee.Salary < role.MinSalary || employee.Salary > role.MaxSalary)
            {
                throw new Exception($"Error: Salary for '{role.Name}' must be between {role.MinSalary} and {role.MaxSalary}.");
            }
            if (role.Name.Contains("Developer") && !department.Name.Contains("Software"))
            {
                throw new Exception($"Error: Role '{role.Name}' is not compatible with '{department.Name}' department.");
            }
            if (string.IsNullOrEmpty(employee.IdentityNumber) || employee.IdentityNumber.Length != 11)
            {
                throw new Exception("Error: Identity Number must be exactly 11 characters.");
            }

            if (string.IsNullOrEmpty(employee.Iban))
                throw new Exception("Error: IBAN is required for payroll processing.");

            var existingEmp = await _employeeDal.GetAsync(e => e.IdentityNumber == employee.IdentityNumber);
            if (existingEmp != null)
            {
                throw new Exception("Error: An employee with this Identity Number already exists.");
            }
            if (string.IsNullOrEmpty(employee.PhoneNumber))
            {
                throw new Exception("Error: Primary phone number is required.");
            }
            var user = await _userService.GetByIdAsync(employee.UserId);
            if (user == null)
            {
                throw new Exception("Error: Associated User not found.");
            }
            if (string.IsNullOrEmpty(user.Email) || !user.Email.Contains("@"))
            {
                throw new Exception("Error: Invalid email format for the associated user.");
            }
            if (employee.PhoneNumber.Length != 10 || employee.PhoneNumber.StartsWith("0"))
            {
                throw new Exception("Error: Phone number must be 10 digits and should not start with 0. (Example: 5xx...)");
            }
            if (string.IsNullOrEmpty(employee.Gender))
            {
                throw new Exception("Error: Gender is required.");
            }
            string genderUpper = employee.Gender.ToUpper();
            if (genderUpper != "MALE" && genderUpper != "FEMALE")
            {
                throw new Exception("Error: Gender must be 'Male' or 'Female'. Other values are not accepted.");
            }
            await _employeeDal.AddAsync(employee);

            if (user != null)
            {
                _mailService.SendEmail(user.Email, "Welcome to Skyline Empire",
                    $"Dear {employee.FirstName}, your employee record has been successfully created. Welcome aboard!");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var userDepartment = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type == "Department")?.Value;
            if (userDepartment != "Management")
            {
                throw new Exception("Unauthorized: Only Management department can terminate employment.");
            }

            var employeeToDelete = await _employeeDal.GetAsync(e => e.Id == id);
            if (employeeToDelete != null)
            {

                await _employeeDal.DeleteAsync(employeeToDelete);
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeDal.GetAsync(e => e.Id == id);
        }

        public async Task<Employee> GetByUserIdAsync(int userId)
        {
            return await _employeeDal.GetAsync(e => e.UserId == userId);
        }

        public async Task<List<EmployeeListDto>> GetFilteredManagementListAsync(int currentUserId)
        {
            var currentUser = await _context.Employees
                .Include(e => e.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(e => e.UserId == currentUserId);

            if (currentUser == null) return new List<EmployeeListDto>();

            var myLevel = currentUser.User.Role.HierarchyLevel;

            var query = _context.Employees
                .Include(e => e.User)
                .ThenInclude(u => u.Role)
                .AsQueryable();

            query = query.Where(e => e.User.Role.HierarchyLevel < 100);

            query = query.Where(e => e.UserId != currentUserId);

            query = query.Where(e => e.User.Role.HierarchyLevel < myLevel);

            return await query.Select(e => new EmployeeListDto
            {
                Id = e.Id,
                FullName = e.FirstName + " " + e.LastName,
                RoleName = e.User.Role.Name,
                SalaryDisplay = e.Salary.ToString("N0") + " TL",
                DepartmentName = e.Department != null ? e.Department.Name : "Unassigned",
                Iban = e.Iban,
                Status = (int)e.Status,
                HierarchyLevel = e.User.Role.HierarchyLevel
            }).ToListAsync();
        }

        public async Task<List<EmployeeListDto>> SearchEmployeesAsync(EmployeeSearchDto filter)
        {
            var employees = await _employeeDal.GetEmployeeDetails();

            var query = employees.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(e =>
                    e.FirstName.Contains(filter.Name, StringComparison.OrdinalIgnoreCase) ||
                    e.LastName.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
            }
            if (filter.DepartmentId.HasValue && filter.DepartmentId > 0)
            {
                query = query.Where(e => e.DepartmentId == filter.DepartmentId.Value);
            }
            if (filter.RoleId.HasValue && filter.RoleId > 0)
            {
                query = query.Where(e => e.RoleId == filter.RoleId.Value);
            }
            if (filter.MinSalary.HasValue)
            {
                query = query.Where(e => e.Salary >= filter.MinSalary.Value);
            }

            if (filter.MaxSalary.HasValue)
            {
                query = query.Where(e => e.Salary <= filter.MaxSalary.Value);
            }
            return query.Select(e => new EmployeeListDto
            {
                Id = e.Id,
                FullName = e.FirstName + " " + e.LastName,
                SalaryDisplay = e.Salary.ToString("N0") + " TL",
                DepartmentName = e.Department != null ? e.Department.Name : "Unassigned",
                RoleName = e.Role != null ? e.Role.Name : "Unassigned",
                Iban = e.Iban,
                Status = (int)e.Status,
                HierarchyLevel = e.Role != null ? e.Role.HierarchyLevel : 0
            }).ToList();
        }

        public async Task UpdateAsync(EmployeeUpdateDto employeeUpdateDto)
        {
            var userDepartment = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type == "Department")?.Value;

            if (userDepartment != "Human Resources" && userDepartment != "Management")
            {
                throw new Exception("Unauthorized: You do not have permission to update employee records.");
            }

            var employeeToUpdate = await _employeeDal.GetAsync(e => e.Id == employeeUpdateDto.Id);
            if (employeeToUpdate == null) throw new Exception("Error: Employee not found!");

            var role = await _roleDal.GetAsync(r => r.Id == employeeUpdateDto.RoleId);
            var department = await _departmentDal.GetAsync(d => d.Id == employeeUpdateDto.DepartmentId);

            if (role == null || department == null)
            {
                throw new Exception("Error: Selected Role or Department not found.");
            }

            if (employeeUpdateDto.Salary < role.MinSalary || employeeUpdateDto.Salary > role.MaxSalary)
            {
                throw new Exception($"Error: New salary must be between {role.MinSalary} and {role.MaxSalary} for '{role.Name}'.");
            }

            if (role.Name.Contains("Developer") && !department.Name.Contains("Software"))
            {
                throw new Exception($"Error: Role '{role.Name}' cannot be assigned to '{department.Name}' department.");
            }

            bool isSalaryChanged = employeeToUpdate.Salary != employeeUpdateDto.Salary;
            bool isRoleOrDeptChanged = employeeToUpdate.RoleId != employeeUpdateDto.RoleId ||
                                       employeeToUpdate.DepartmentId != employeeUpdateDto.DepartmentId;

            employeeToUpdate.FirstName = employeeUpdateDto.FirstName;
            employeeToUpdate.LastName = employeeUpdateDto.LastName;
            employeeToUpdate.Salary = employeeUpdateDto.Salary;
            employeeToUpdate.DepartmentId = employeeUpdateDto.DepartmentId;
            employeeToUpdate.RoleId = employeeUpdateDto.RoleId;
            employeeToUpdate.Iban = employeeUpdateDto.Iban;

            await _employeeDal.UpdateAsync(employeeToUpdate);

            if (isSalaryChanged || isRoleOrDeptChanged)
            {
                string message = isRoleOrDeptChanged
                    ? $"Tebrikler! Yeni pozisyonunuz '{role.Name}' ({department.Name}) olarak mühürlenmiştir. Yeni Maaş: {employeeUpdateDto.Salary} TL"
                    : $"Maaşınız güncellenmiştir. Yeni maaşınız: {employeeUpdateDto.Salary} TL";

                await _notificationService.SendNotificationAsync(
                    message: message,
                    type: isRoleOrDeptChanged ? NotificationTypes.Promotion : NotificationTypes.SalaryChange,
                    targetActionId: employeeToUpdate.Id,
                    targetUserId: employeeToUpdate.UserId
                );
            }

            var accountingDept = await _departmentDal.GetAsync(d =>
    d.Name.Trim().ToLower() == "accounting" || d.Name.Trim().ToLower() == "muhasebe");

            if (isSalaryChanged || isRoleOrDeptChanged)
            {
                await _notificationService.SendNotificationAsync(
                    message: $"Mali Duyuru: {employeeToUpdate.FirstName} {employeeToUpdate.LastName} için maaş/pozisyon güncellemesi yapıldı. Ay sonu payroll verileri günceldir.",
                    type: NotificationTypes.Promotion,
                    targetActionId: employeeToUpdate.Id,
                    targetDepartmentId: accountingDept?.Id
                );
            }
        }

        public async Task UpdateStatusAsync(int id, EmployeeStatus status)
        {
            var employee = await _employeeDal.GetAsync(e => e.Id == id);
            if (employee == null) throw new Exception("Personel bulunamadı!");

            employee.Status = status;
            await _employeeDal.UpdateAsync(employee);

            var termination = await _terminationDal.GetAsync(t => t.EmployeeId == id && t.Status == TerminationStatus.ManagerApproved);

            if (termination != null)
            {
                termination.Status = TerminationStatus.HRProcessed;

                termination.HrApprovalDate = DateTime.Now;

                decimal finalAmount = await _terminationService.CalculateSeveranceAndNotice(employee);
                termination.CalculatedAmount = finalAmount;

                await _terminationDal.UpdateAsync(termination);

                var accountingDept = await _departmentDal.GetAsync(d =>
                    d.Name.Trim().ToLower() == "accounting" || d.Name.Trim().ToLower() == "muhasebe");

                if (accountingDept != null)
                {
                    await _notificationService.SendNotificationAsync(
                        message: $"{employee.FirstName} {employee.LastName} için İK süreci tamamlandı. Ödeme ve dosya kapatma bekleniyor.",
                        type: NotificationTypes.TerminationProcess, 
                        targetActionId:employee.Id,
                        targetDepartmentId: accountingDept.Id 
                    );
                }
            }
        }
    }
}
