using MediatR;
using Modules.UserManagement.App.Services.RefreshToken;
using Modules.UserManagement.Infrastructure.DataAccessLayer.UoT;
using SharedUtils.Jwt;
using SharedUtils.Time;

namespace Modules.UserManagement.App.Commands.RefreshLogin;

public sealed class RefreshLoginCommandHandler(
    IUnitOfWork _uot,
    IJwtService _jwtService,
    IRefreshTokenService _tokenService,
    IClock _clock)
    : IRequestHandler<RefreshLoginCommand, RefreshLoginCommandResponse>
{
    public async Task<RefreshLoginCommandResponse> Handle(RefreshLoginCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = request.RefreshToken;
        var hashedRefreshToken = _tokenService.HashToken(refreshToken);

        var account = await _uot.Accounts.GetByRefreshTokenAsync(hashedRefreshToken);
        if (account == null)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        var validToken = account.RefreshTokens.FirstOrDefault(rt => rt.HashedToken == hashedRefreshToken);
        if (validToken == null || validToken.IsRevoked || validToken.IsExpired())
        {
            throw new UnauthorizedAccessException("Refresh token is invalid or expired.");
        }

        var newAccessToken = _jwtService.GenerateJwtToken(account.Id, account.Username, account.Email);
        var newRefreshToken = _tokenService.GenerateToken();

        account.RevokeRefreshToken(refreshToken);
        account.AddRefreshToken(
            _tokenService.HashToken(newRefreshToken),
            _clock.Now.AddDays(7),
            "device-info",
            "ip-address");
        account.RemoveExpiredOrUsedTokens();

        await _uot.Accounts.UpdateAsync(account);

        await _uot.SaveAsync();

        return new RefreshLoginCommandResponse(newAccessToken, newRefreshToken);
    }
}
