using NotificationGateway.Domain;

namespace NotificationGateway.Application.Router;

public interface INotificationRoute
{
    NotificationChannel Channel { get; }
    Task PublishAsync(Notification notification, CancellationToken ct);
}
