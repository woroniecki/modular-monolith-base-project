using MediatR;
using Microsoft.Extensions.Logging;
using Modules.UserManagement.IntegrationEvents.Events;

namespace Modules.UserManagement.App.EventHandlers.Domain;
public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredIntegrationEvent>
{
    private readonly ILogger<UserRegisteredEventHandler> _logger;

    public UserRegisteredEventHandler(ILogger<UserRegisteredEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling integration event: {username} {id}", notification.UserName, notification.UserId);
        return Task.CompletedTask;
    }
}
