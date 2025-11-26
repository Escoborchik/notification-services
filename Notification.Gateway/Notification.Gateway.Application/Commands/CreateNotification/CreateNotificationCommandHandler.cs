using NotificationGateway.Application.Interfaces;
using NotificationGateway.Domain;

namespace NotificationGateway.Application.Commands.CreateNotification;

public class CreateNotificationCommandHandler(
    INotificationEventPublisher publisher)
{
    public async Task<Guid> Handle(CreateNotificationCommand command)
    {
        var notification = new Notification(
            Guid.NewGuid(),
            command.Channel,
            command.Recipient,
            command.Content
        );


        await publisher.PublishNotificationCreatedAsync(notification.Id, notification.Channel);

        return notification.Id;
    }
}
