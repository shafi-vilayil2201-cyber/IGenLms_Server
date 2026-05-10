

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IGenServer.Infrastructure.Authentication;
public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;
    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var jwtKey = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWt key is not configured.");
        
        var jwtIssuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt issuer is not configured.");

        var jwtAudience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt audience is not configured.");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, user.Role.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience:jwtAudience,
            claims:claims,
            expires:DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}