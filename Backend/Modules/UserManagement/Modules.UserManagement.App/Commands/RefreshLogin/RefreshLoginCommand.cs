using MediatR;

namespace Modules.UserManagement.App.Commands.RefreshLogin;

public record RefreshLoginCommand(string RefreshToken) : IRequest<RefreshLoginCommandResponse>;

public record RefreshLoginCommandResponse(string AccessToken, string NewRefreshToken);