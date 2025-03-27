using MediatR;

namespace Modules.UserManagement.App.Commands.Register;

public record RegisterCommand(string Username, string Email, string Password) : IRequest<Unit>;