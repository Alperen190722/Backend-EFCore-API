using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Concrete;

namespace D41_SkylinePayroll.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _departmentService.GetDepartmentDetails());

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Department department)
        {
            if (department == null) return BadRequest("Veri boş geldi!");
            await _departmentService.AddAsync(department);
            return Ok("Departman başarıyla eklendi");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]Department department)
        {
            await _departmentService.UpdateAsync(department);
            return Ok("Departman başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _departmentService.GetByIdAsync(id);
            if (dept == null) return NotFound("Departman bulunamadı!");

            await _departmentService.DeleteAsync(dept);
            return Ok("Department silme başarılı.");
        }
    }
}
