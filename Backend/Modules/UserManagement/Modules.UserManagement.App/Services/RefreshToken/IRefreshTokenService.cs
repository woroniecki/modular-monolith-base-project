namespace Modules.UserManagement.App.Services.RefreshToken;
public interface IRefreshTokenService
{
    string HashToken(string token);
    string GenerateToken(int length = 64);
}
