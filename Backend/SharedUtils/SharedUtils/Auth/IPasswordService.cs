namespace UserAuth.App.Services.Password;
public interface IPasswordService
{
    string HashPassword(string password);
}
