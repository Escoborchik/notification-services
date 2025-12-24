using Notifications.Contracts.Abstractions;
using Notifications.Contracts.Enums;

namespace Notifications.Contracts.Events;

public record EmailNotificationEvent : NotificationEventBase
{
    public string Email { get; init; } = string.Empty;
    public override NotificationChannel NotificationChannel => NotificationChannel.Email; 
}
