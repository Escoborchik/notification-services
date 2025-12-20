using MassTransit;
using NotificationGateway.Application.Commands.CreateNotification;
using NotificationGateway.Domain;

namespace NotificationGateway.Application.Router;

public class EmailNotificationRoute : INotificationRoute
{
    private readonly IPublishEndpoint _publish;

    public EmailNotificationRoute(IPublishEndpoint publish)
    {
        _publish = publish;
    }

    public NotificationChannel Channel => NotificationChannel.Email;

    public Task PublishAsync(Notification n, CancellationToken ct)
    {
        return _publish.Publish(
        new EmailNotificationEvent(
            n.Id,
            n.Recipient.Email!,
            n.Content.Subject,
            n.Content.Text,
            DateTime.UtcNow),
        ct);
    }
}
