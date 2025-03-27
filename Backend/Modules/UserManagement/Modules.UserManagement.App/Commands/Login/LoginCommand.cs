using MediatR;

namespace Modules.UserManagement.App.Commands.Login;

public record LoginCommand(string Username, string Password) : IRequest<LoginCommandResponse>;

public record LoginCommandResponse(string AccessToken, string RefreshToken);