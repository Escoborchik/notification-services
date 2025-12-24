using Notifications.Contracts.Abstractions;
using Notifications.Contracts.Enums;

namespace Notifications.Contracts.Events;

public record SmsNotificationEvent : NotificationEventBase
{
    public string PhoneNumber { get; init; } = string.Empty;
    public override NotificationChannel NotificationChannel => NotificationChannel.Sms;
}