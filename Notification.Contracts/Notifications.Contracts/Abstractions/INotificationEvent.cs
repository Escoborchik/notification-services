namespace Notifications.Contracts.Abstractions;

public interface INotificationEvent
{
    Guid NotificationId { get; }
    DateTime OccurredAt { get; }
}
