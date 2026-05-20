using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace D41_SkylinePayroll.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeSearchDto filter)
        {
            var result = await _employeeService.SearchEmployeesAsync(filter);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            await _employeeService.AddAsync(employee);
            return Ok("Personel ekleme başarılı!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(EmployeeUpdateDto employeeUpdateDto)
        {
            await _employeeService.UpdateAsync(employeeUpdateDto);
            return Ok("Personel Güncellendi!");
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(int id, EmployeeStatus status)
        {
            await _employeeService.UpdateStatusAsync(id, status);
            return Ok("Personel Statüsü Güncellendi!");
        }

        [HttpGet("get-filtered-management-list")]
        public async Task<IActionResult> GetFilteredManagementList()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("Kullanıcı kimliği doğrulanamadı.");
            int currentUserId = int.Parse(userIdClaim.Value);
            var employees = await _employeeService.GetFilteredManagementListAsync(currentUserId);
            return Ok(employees);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound("Personel Bulunamadı.");
            }

            await _employeeService.DeleteAsync(id);
            return Ok("Personel başarıyla silindi.");
        }
    }
}
