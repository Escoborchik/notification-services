using NotificationGateway.Domain;
using NotificationGateway.Domain.Enums;

namespace NotificationGateway.Application.Router;

public interface INotificationRoute
{
    NotificationChannel Channel { get; }
    Task PublishAsync(Notification notification, CancellationToken ct);
}
