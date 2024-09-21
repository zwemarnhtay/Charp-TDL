using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TDL.Application.DTOs;

namespace TDL.Application.Helpers;

public class JwtGenerator
{
  private readonly IConfiguration _config;

  public JwtGenerator(IConfiguration config)
  {
    _config = config;
  }

  public string GenerateToken(UserDto user)
  {
    string key = _config.GetSection("JWT:Key").Value!;
    string issuer = _config.GetSection("JWT:Issuer").Value!;
    string audience = _config.GetSection("JWT:Audience").Value!;

    var claims = new[]
          {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
          };

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    var expires = DateTime.UtcNow.AddHours(7);

    var token = new JwtSecurityToken(
        issuer,
        audience,
        claims,
        expires: expires,
        signingCredentials: creds
    );

    string jwt = new JwtSecurityTokenHandler().WriteToken(token);

    return jwt;
  }
}
