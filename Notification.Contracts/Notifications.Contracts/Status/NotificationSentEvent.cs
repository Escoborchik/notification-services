namespace Notifications.Contracts.Status;

public record NotificationSentEvent(
    Guid NotificationId,
    string Channel,
    DateTime SentAt
);
