using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Concrete;

namespace D41_SkylinePayroll.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getbydepartment")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            
            var allRoles = await _roleService.GetRoleDetails();


            var filteredRoles = allRoles.Where(r => r.DepartmentId == departmentId).ToList();

            return Ok(filteredRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Role role)
        {
            await _roleService.AddAsync(role);
            return Ok("Rol Eklendi!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Role role)
        {
            await _roleService.UpdateAsync(role);
            return Ok("Rol Güncellendi!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound("Rol bulunamadı!");

            await _roleService.DeleteAsync(role);
            return Ok("Rol Silindi!");
        }
    }
}
