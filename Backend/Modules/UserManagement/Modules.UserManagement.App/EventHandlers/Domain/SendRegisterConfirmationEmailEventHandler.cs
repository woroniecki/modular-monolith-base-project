using MediatR;
using Microsoft.Extensions.Logging;

namespace Modules.UserManagement.App.EventHandlers.Domain;
public class SendRegisterConfirmationEmailEventHandler : INotificationHandler<AccountCreatedEvent>
{
    private readonly ILogger<SendRegisterConfirmationEmailEventHandler> _logger;

    public SendRegisterConfirmationEmailEventHandler(ILogger<SendRegisterConfirmationEmailEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending confirmation email to {id}", notification.accountId);
        return Task.CompletedTask;
    }
}
