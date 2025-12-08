using NotificationGateway.Domain;

namespace NotificationGateway.Application.Interfaces;

public interface INotificationEventPublisher
{
    Task PublishNotificationCreatedAsync(Guid notificationId, NotificationChannel channel);
}
