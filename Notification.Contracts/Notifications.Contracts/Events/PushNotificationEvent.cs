using Notifications.Contracts.Abstractions;
using Notifications.Contracts.Enums;

namespace Notifications.Contracts.Events;

public record PushNotificationEvent : NotificationEventBase
{
    public string PushToken { get; init; } = string.Empty;
    public override NotificationChannel NotificationChannel => NotificationChannel.Push;
}