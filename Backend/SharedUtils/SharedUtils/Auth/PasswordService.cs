using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace UserAuth.App.Services.Password;
public class PasswordService : IPasswordService
{
    private readonly IConfiguration _configuration;

    public PasswordService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string HashPassword(string password)
    {
        var salt = Encoding.UTF8.GetBytes(_configuration["Salt"]);

        using (var sha256 = new SHA256Managed())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }
    }
}
