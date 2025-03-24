namespace SharedUtils.Jwt;
public interface IJwtService
{
    string GenerateJwtToken(Guid userId, string userName, string email);
}
