using MediatR;

namespace Modules.UserManagement.App.Commands.Login;
// Include properties to be used as input for the command
public record LoginCommand(string Username, string Password) : IRequest<LoginCommandResponse>;

public record LoginCommandResponse(string accessToken);