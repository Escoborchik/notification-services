using CSharpFunctionalExtensions;
using Framework.Abstractions;
using MassTransit;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Commands.CreateNotification;

public class SendNotificationCommandHandler(
    INotificationRepository notificationsRepository,
    INotificationTransactionManager unitOfWork,
     INotificationRouter router) : ICommandHandler<Guid, SendNotificationCommand>
{
    public async Task<Result<Guid, Errors>> Execute(SendNotificationCommand command, CancellationToken ct)
    {
        var recipient = new Recipient(
            command.Recipient.Email,
            command.Recipient.PhoneNumber,
            command.Recipient.PushToken
        );

        var content = new Content(command.Content.Subject, command.Content.Text);

        var notification = Notification.Create(
            command.Channel,
            recipient,
            content
        );

        await notificationsRepository.AddAsync(notification, ct);

        await router.RouteAsync(notification, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return notification.Id;
    }
}

public interface INotificationRouter
{
    Task RouteAsync(Notification notification, CancellationToken ct);
}

public interface INotificationEvent
{
    Guid NotificationId { get; }
}

public record EmailNotificationEvent(
    Guid NotificationId,
    string Email,
    string Subject,
    string Message
) : INotificationEvent;

public interface INotificationRoute
{
    NotificationChannel Channel { get; }
    Task PublishAsync(Notification notification, CancellationToken ct);
}

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
        return _publish.Publish(new EmailNotificationEvent(
            n.Id,
            n.Recipient.Email,
            n.Content.Subject,
            n.Content.Text
        ), ct);
    }

    public class NotificationRouter : INotificationRouter
    {
        private readonly Dictionary<NotificationChannel, INotificationRoute> _routes;

        public NotificationRouter(IEnumerable<INotificationRoute> routes)
        {
            _routes = routes.ToDictionary(x => x.Channel, x => x);
        }

        public Task RouteAsync(Notification notification, CancellationToken ct)
        {
            if (_routes.TryGetValue(notification.Channel, out var route))
                return route.PublishAsync(notification, ct);

            throw new NotSupportedException($"Channel {notification.Channel} not supported.");
        }
    }
}