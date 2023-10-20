using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.Department;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers
{
    public class DepartmentController : ApplicationController
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var deps = await _departmentService.GetAllDepartments();
            return Ok(deps);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            await _departmentService.CreateDepartment(createDepartmentDto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            return Ok(department);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentById([FromRoute] int id)
        {
            await _departmentService.DeleteDepartment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            await _departmentService.UpdateDepartment(id, updateDepartmentDto);
            return Ok();
        }

    }
}
