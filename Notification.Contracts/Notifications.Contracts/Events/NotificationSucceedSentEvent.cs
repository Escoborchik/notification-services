namespace Notifications.Contracts.Events;

public record NotificationSucceedSentEvent(
    Guid NotificationId,
    DateTime SentAt
);
