using System.Security.Cryptography;
using System.Text;

namespace Modules.UserManagement.App.Services.RefreshTokenService;
public class RefreshTokenService : IRefreshTokenService
{
    public string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public string GenerateToken(int length = 64)
    {
        var bytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes).Substring(0, length);
    }
}
