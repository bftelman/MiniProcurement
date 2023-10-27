using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Middleware;

public class AuthorizationMiddleware
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthorizationMiddleware> _logger;
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration,
        ILogger<AuthorizationMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader != null)
        {
            var token = authHeader.Split(" ").Last();
            await AttachUserIdToContext(context, userService, token);
        }

        await _next(context);
    }

    private async Task AttachUserIdToContext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenKey"));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = "localhost:telman",
                ValidAudience = "MiniProcurementApp",
                ValidateIssuer = true,
                ValidateAudience = true
            }, out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var id = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);
            var user = await userService.GetUserByIdRaw(id);
            context.Items.Add("User", user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}