using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Dtos;
using System.Security.Claims;

namespace D41_SkylinePayroll.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        public UsersController(IUserService userService, IEmployeeService employeeService)
        {
            _userService = userService;
            _employeeService = employeeService;
        }

        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound("User not found!");


            var employee = await _employeeService.GetByIdAsync(userId);

            if (employee == null) return NotFound("Employee information is missing!");

            var userDto = new UserDetailDto
            {
                Id = user.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = user.Email,            
                DepartmentId = employee.DepartmentId 
            };

            return Ok(userDto);
        }


        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(UserDetailDto userUpdateDto)
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _userService.UpdateUserProfileAsync(userId, userUpdateDto);

            if (result) return Ok("Profile updated successfully!");

            return BadRequest("Update failed.");
        }
    }
}
