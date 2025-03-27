using MediatR;
using Modules.UserManagement.App.Services.Password;
using Modules.UserManagement.App.Services.RefreshToken;
using Modules.UserManagement.Infrastructure.DataAccessLayer.UoT;
using SharedUtils.Jwt;
using SharedUtils.Time;

namespace Modules.UserManagement.App.Commands.Login;
public sealed class LoginCommandHandler(
    IUnitOfWork _uot,
    IPasswordService _passwordService,
    IJwtService _jwtService,
    IRefreshTokenService _tokenService,
    IClock _clock)
    : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordService.HashPassword(request.Password);

        var account = await _uot.Accounts.GetByUsernameAsync(request.Username);

        if (account != null && hashedPassword == account.Password)
        {
            var newRefreshToken = _tokenService.GenerateToken();

            account.AddRefreshToken(
                _tokenService.HashToken(newRefreshToken),
                _clock.Now.AddDays(7),
                "device-info",
                "ip-address");

            await _uot.Accounts.UpdateAsync(account);

            return new LoginCommandResponse(
                _jwtService.GenerateJwtToken(account.Id, account.Username, account.Email),
                newRefreshToken
                );
        }

        await _uot.SaveAsync();

        throw new Exception("Invalid username or password");
    }
}