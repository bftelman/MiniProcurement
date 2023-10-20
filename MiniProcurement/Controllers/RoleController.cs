using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.Role;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers
{
    public class RoleController : ApplicationController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRoleDto>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById([FromRoute] int id)
        {
            var role = await _roleService.GetRoleById(id);
            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoleById([FromRoute] int id)
        {
            await _roleService.DeleteRole(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole([FromRoute] int id, [FromBody] string roleName)
        {
            await _roleService.UpdateRole(id, roleName);
            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult> CreateRole([FromBody] string roleName)
        {
            await _roleService.CreateRole(roleName);
            return NoContent();
        }
    }
}
