using Notifications.Contracts.Abstractions;

namespace Notifications.Contracts.Email;

public record EmailNotificationEvent(
    Guid NotificationId,
    string Email,
    string Subject,
    string Body,
    DateTime OccurredAt
) : INotificationEvent;
