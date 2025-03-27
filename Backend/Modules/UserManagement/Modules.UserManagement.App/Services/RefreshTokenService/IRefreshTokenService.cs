namespace Modules.UserManagement.App.Services.RefreshTokenService;
public interface IRefreshTokenService
{
    string HashToken(string token);
    string GenerateToken(int length = 64);
}
