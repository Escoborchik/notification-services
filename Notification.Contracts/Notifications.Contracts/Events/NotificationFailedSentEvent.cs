namespace Notifications.Contracts.Events;

public record NotificationFailedSentEvent(
    Guid NotificationId,
    string ErrorMessage,
    DateTime FailedAt
);
