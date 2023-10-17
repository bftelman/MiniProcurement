using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Concretes;
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
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }
    }
}
