using SharedUtils.Events;

namespace Modules.UserManagement.IntegrationEvents.Events;

public class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(Guid userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }

    public Guid UserId { get; }
    public string UserName { get; }
}
