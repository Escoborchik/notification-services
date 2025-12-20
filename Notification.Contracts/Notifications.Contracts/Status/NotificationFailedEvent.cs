namespace Notifications.Contracts.Status;

public record NotificationFailedEvent(
    Guid NotificationId,
    string Channel,
    string ErrorCode,
    string ErrorMessage,
    DateTime FailedAt
);
