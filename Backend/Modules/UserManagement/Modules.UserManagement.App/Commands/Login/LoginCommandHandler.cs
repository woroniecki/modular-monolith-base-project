using MediatR;
using Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;
using SharedUtils.Jwt;
using UserAuth.App.Services.Password;

namespace Modules.UserManagement.App.Commands.Login;
public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IAccountRepository _accountRepo;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IAccountRepository accountRepo, IPasswordService passwordService, IJwtService jwtService)
    {
        _accountRepo = accountRepo;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordService.HashPassword(request.Password);

        var account = await _accountRepo.GetByUsernameAsync(request.Username);

        if (account != null && hashedPassword == account.Password)
        {
            return new LoginCommandResponse(
                _jwtService.GenerateJwtToken(account.Id, account.Username, account.Email)
                );
        }

        throw new Exception("Invalid username or password");
    }
}