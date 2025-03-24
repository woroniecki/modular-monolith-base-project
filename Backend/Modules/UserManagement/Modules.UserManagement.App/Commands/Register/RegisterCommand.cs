using MediatR;

namespace Modules.UserManagement.App.Commands.Register;
// Include properties to be used as input for the command
public record RegisterCommand(string Username, string Email, string Password) : IRequest<Unit>;