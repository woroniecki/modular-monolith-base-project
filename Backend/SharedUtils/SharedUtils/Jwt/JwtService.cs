using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SharedUtils.Auth;

namespace SharedUtils.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(Guid userId, string userName, string email)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
        var expirationDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"]));
        return GenerateToken(
            new ClaimsIdentity([
                    new Claim(ClaimType.UserId, userId.ToString()),
                    new Claim(ClaimType.Username, userName),
                    new Claim(ClaimType.Email, email)
                ]),
            tokenHandler, key, expirationDate);
    }

    private static string GenerateToken(ClaimsIdentity claimsIdentity, JsonWebTokenHandler tokenHandler, byte[] key, DateTime expirationDate)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = expirationDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        return tokenHandler.CreateToken(tokenDescriptor);
    }
}
