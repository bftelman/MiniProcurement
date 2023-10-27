using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.Authentication;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers;

public class AuthenticationController : ApplicationController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        await _authenticationService.Register(registerDto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authenticationService.Login(loginDto);
        return Ok(result);
    }
}