using Notifications.Contracts.Abstractions;

namespace Notifications.Contracts.Sms;

public record SmsNotificationEvent(
    Guid NotificationId,
    string Phone,
    string Message,
    DateTime OccurredAt
) : INotificationEvent;
