using Microsoft.EntityFrameworkCore;
using SharedUtils.Domain.Entities;

namespace Modules.UserManagement.Domain.Aggregates.Account.Entities;

[Index(nameof(HashedToken), IsUnique = true)]
public class RefreshToken : Entity
{
    public string HashedToken { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public string Device { get; set; }

    private RefreshToken() { }

    public RefreshToken(string token, DateTime expiryDate, string device, string ipAddress)
    {
        HashedToken = token;
        ExpiryDate = expiryDate;
        Device = device;
        IsRevoked = false;
    }

    public void Revoke()
    {
        IsRevoked = true;
    }

    public bool IsExpired()
    {
        return ExpiryDate < DateTime.UtcNow;
    }
}
