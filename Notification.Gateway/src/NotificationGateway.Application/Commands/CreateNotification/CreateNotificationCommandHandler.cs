using CSharpFunctionalExtensions;
using Framework.Abstractions;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Application.Router;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Commands.CreateNotification;



public sealed class CreateNotificationCommandHandler(
    INotificationRepository repository,
    INotificationTransactionManager unitOfWork,
    INotificationRouter router)
        : ICommandHandler<Guid, CreateNotificationCommand>
{
    public async Task<Result<Guid, Errors>> Execute(
        CreateNotificationCommand command,
        CancellationToken ct)
    {
        // 1. Recipient
        var recipientResult = Recipient.Create(
            command.Recipient.Email,
            command.Recipient.PhoneNumber,
            command.Recipient.PushToken);

        if (recipientResult.IsFailure)
            return recipientResult.Error.ToErrors();

        // 2. Content
        var contentResult = Content.Create(
            command.Content.Subject,
            command.Content.Text);

        if (contentResult.IsFailure)
            return contentResult.Error.ToErrors();

        // 3. Notification
        var notificationResult = Notification.Create(
            command.Channel,
            recipientResult.Value,
            contentResult.Value);

        if (notificationResult.IsFailure)
            return notificationResult.Error.ToErrors();

        var notification = notificationResult.Value;

        // 4. Persist
        await repository.AddAsync(notification, ct);

        // 5. Publish (через Outbox, НЕ в RabbitMQ напрямую)
        await router.RouteAsync(notification, ct);

        // 6. Domain transition
        var queuedResult = notification.MarkQueued();
        if (queuedResult.IsFailure)
            return queuedResult.Error.ToErrors();

        // 7. Commit (Notification + OutboxMessages)
        await unitOfWork.SaveChangesAsync(ct);

        return notification.Id;
    }
}


public record EmailNotificationEvent(
    Guid NotificationId,
    string Email,
    string Subject,
    string Message,
    DateTime OccurredAtUtc
);
