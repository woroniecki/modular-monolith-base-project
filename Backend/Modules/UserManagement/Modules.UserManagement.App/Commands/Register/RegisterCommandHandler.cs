using MediatR;
using Modules.UserManagement.Domain.Aggregates;
using Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;
using Modules.UserManagement.IntegrationEvents.Events;
using SharedUtils.Events;
using UserAuth.App.Services.Password;

namespace Modules.UserManagement.App.Commands.Register;
public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IAccountRepository _accountRepo;
    private readonly IPasswordService _passwordService;
    private readonly IEventsQueueService _eventsQueue;

    public RegisterCommandHandler(
        IAccountRepository accountRepo,
        IPasswordService passwordService,
        IEventsQueueService eventsQueue)
    {
        _accountRepo = accountRepo;
        _passwordService = passwordService;
        _eventsQueue = eventsQueue;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _accountRepo.AddAsync(
            new Account(
                request.Username,
                request.Email,
                _passwordService.HashPassword(request.Password)
                ));

        _eventsQueue.Add(new UserRegisteredIntegrationEvent(Guid.NewGuid(), request.Username));

        return Unit.Value;
    }
}