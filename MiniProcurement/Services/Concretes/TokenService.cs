using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration configuration)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.UserName),
            new(JwtRegisteredClaimNames.GivenName, user.FullName)
        };

        foreach (var role in user.Roles) claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            IssuedAt = DateTime.Now,
            Issuer = "localhost:telman",
            Audience = "MiniProcurementApp"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}