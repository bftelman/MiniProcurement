using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.User;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers;

public class UserController : ApplicationController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();
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

    [HttpPut("{id}/update")]
    public async Task<ActionResult> UpdateUserName([FromRoute] int id, [FromBody] UpdateNameDto updateNameDto)
    {
        await _userService.UpdateUserName(updateNameDto, id);
        return NoContent();
    }

    [HttpPut("{id}/assignRole")]
    public async Task<ActionResult> AssignRole([FromRoute] int id, [FromBody] AssignRoleToUserDto assignRoleToUserDto)
    {
        await _userService.AssignRole(assignRoleToUserDto, id);
        return NoContent();
    }

    [HttpPut("{id}/assignDepartment")]
    public async Task<ActionResult> AssignDepartment([FromRoute] int id,
        [FromBody] AssignDepartmentToUserDto assignDepartmentToUserDto)
    {
        await _userService.AssignDepartment(id, assignDepartmentToUserDto);
        return Ok();
    }
}