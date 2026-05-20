using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace D41_SkylinePayroll.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TerminationsController : ControllerBase
    {
        private readonly ITerminationService _terminationService;
        public TerminationsController(ITerminationService terminationService)
        {
            _terminationService = terminationService;
        }

        [HttpPost("initiate-termination/{id}")]
        public async Task<IActionResult> Initiate(int id, [FromBody] string reason)
        {
            var result = await _terminationService.InitiateTermination(id, reason);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        
        [HttpPost("approve-hr/{employeeId}")]
        public async Task<IActionResult> ApproveHR(int employeeId)
        {
            var result = await _terminationService.ApproveHRCalculation(employeeId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        
        [HttpPost("employee-approve/{employeeId}")]
        public async Task<IActionResult> EmployeeApprove(int employeeId)
        {
            
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();
            var result = await _terminationService.EmployeeApprove(employeeId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        
        [HttpGet("get-detail-for-employee/{employeeId}")]
        public async Task<IActionResult> GetDetailForEmployee(int employeeId)
        {
            
            var result = await _terminationService.GetTerminationDetailForEmployee(employeeId);

            
            return result.Success ? Ok(result) : BadRequest(result);
        }

        
        [HttpGet("get-pending-accounting")]
        public async Task<IActionResult> GetPendingAccounting()
        {
            var result = await _terminationService.GetPendingAccountingPayments();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        
        [HttpPost("finalize-termination/{EmployeeId}")]
        public async Task<IActionResult> Finalize(int EmployeeId, [FromBody] int typeValue)
        {
            TerminationType type = (TerminationType)typeValue;
            var result = await _terminationService.FinalizeTermination(EmployeeId, type);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        
        [HttpPost("submit-resignation")]
        public async Task<IActionResult> SubmitResignation([FromBody] ResignationDto resignationDto)
        {
            
            var result = await _terminationService.SubmitResignation(resignationDto.EmployeeId, resignationDto.Reason, resignationDto.DepartmentId);

            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        
        [HttpPost("approve-resignation/{employeeId}")]
        public async Task<IActionResult> ApproveResignation(int employeeId)
        {
            
            var userDepartment = HttpContext.User.FindFirst("Department")?.Value;

            if (!string.Equals(userDepartment, "Management", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(userDepartment, "Yönetim", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(403, Result<string>.Error("Bu işlem için Yönetim yetkisi yetersiz!"));
            }

            var result = await _terminationService.ApproveResignation(employeeId);

            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _terminationService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}