using Notifications.Contracts.Enums;

namespace Notifications.Contracts.Abstractions;

public abstract record NotificationEventBase : INotificationEvent
{
    public Guid NotificationId { get; init; }
    public string Subject { get; init; } = null!;
    public string Body { get; init; } = null!;
    public NotificationType NotificationType { get; init; }
    public DateTime OccurredAt { get; init; }

    public abstract NotificationChannel NotificationChannel { get; }
}