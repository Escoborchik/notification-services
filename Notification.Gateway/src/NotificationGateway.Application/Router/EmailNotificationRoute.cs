using MassTransit;
using NotificationGateway.Domain;
using Notifications.Contracts.Events;
using NotificationChannel = NotificationGateway.Domain.Enums.NotificationChannel;
using NotificationType = Notifications.Contracts.Enums.NotificationType;

namespace NotificationGateway.Application.Router;

public class EmailNotificationRoute(IPublishEndpoint publish) : INotificationRoute
{
    public NotificationChannel Channel => NotificationChannel.Email;

    public Task PublishAsync(Notification n, CancellationToken ct)
    {
        return publish.Publish(
        new EmailNotificationEvent()
        {
            NotificationId = n.Id,
            Email = n.Recipient.Email!,
            Subject = n.Content.Subject,
            Body = n.Content.Body,
            NotificationType = (NotificationType)n.Type,
            OccurredAt = DateTime.UtcNow
        }, ct);
    }
}
