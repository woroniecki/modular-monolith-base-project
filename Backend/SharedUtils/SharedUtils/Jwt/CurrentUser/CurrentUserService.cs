using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SharedUtils.Auth;

namespace SharedUtils.Jwt.CurrentUser;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimType.UserId);
            return Guid.TryParse(userIdString, out var guid)
                ? guid
                : throw new InvalidOperationException("User ID cannot be null.");
        }
    }
}
