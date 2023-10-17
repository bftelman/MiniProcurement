using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserById([FromRoute] int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponseDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var result = await _userService.CreateUser(createUserDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserName([FromRoute] int id, [FromBody] string fullName)
        {
            await _userService.UpdateUserName(fullName, id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AssignRole([FromRoute] int id, [FromBody] string roleName)
        {
            await _userService.AssignRole(roleName, id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AssignDepartment([FromRoute] int id, [FromBody] int departmentId)
        {
            await _userService.AssignDepartment(id, departmentId);
            return NoContent();
        }
    }
}
