using MediatR;
using Modules.UserManagement.Domain.Aggregates.Account;
using Modules.UserManagement.Infrastructure.DataAccessLayer.UoT;
using Modules.UserManagement.IntegrationEvents.Events;
using SharedUtils.Events;
using UserAuth.App.Services.Password;

namespace Modules.UserManagement.App.Commands.Register;
public sealed class RegisterCommandHandler(
    IUnitOfWork _uot,
    IPasswordService _passwordService,
    IEventsQueueService _eventsQueue)
    : IRequestHandler<RegisterCommand, Unit>
{
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _uot.Accounts.AddAsync(
            new Account(
                request.Username,
                request.Email,
                _passwordService.HashPassword(request.Password)
                ));

        _eventsQueue.Add(new UserRegisteredIntegrationEvent(Guid.NewGuid(), request.Username));

        await _uot.SaveAsync();

        return Unit.Value;
    }
}