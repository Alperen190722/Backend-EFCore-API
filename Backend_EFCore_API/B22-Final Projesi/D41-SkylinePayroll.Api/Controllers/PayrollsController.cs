using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkylinePayroll.Business.Abstract;

namespace D41_SkylinePayroll.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollsController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollsController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }
        [HttpPost("run-monthly")]
        public async Task<IActionResult> RunMonthlyPayroll()
        {
            var result = await _payrollService.RunMonthlyPayroll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("history/{employeeId}")]
        public async Task<IActionResult> GetHistory(int employeeId)
        {
            var result = await _payrollService.GetEmployeePayrollHistory(employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("getsummary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _payrollService.GetDashboardSummary();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
