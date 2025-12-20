using Notifications.Contracts.Abstractions;

namespace Notifications.Contracts.Push;

public record PushNotificationEvent(
    Guid NotificationId,
    string PushToken,
    string Title,
    string Message,
    DateTime OccurredAt
) : INotificationEvent;