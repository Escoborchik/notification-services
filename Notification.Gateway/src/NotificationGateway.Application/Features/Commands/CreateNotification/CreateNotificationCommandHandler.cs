using CSharpFunctionalExtensions;
using Framework.Abstractions;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Application.Router;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Features.Commands.CreateNotification;

public sealed class CreateNotificationCommandHandler(
    INotificationRepository repository,
    INotificationTransactionManager unitOfWork,
    INotificationDispatcher dispatcher)
        : ICommandHandler<Guid, CreateNotificationCommand>
{
    public async Task<Result<Guid, Errors>> Execute(
        CreateNotificationCommand command,
        CancellationToken ct)
    {
        var recipientResult = Recipient.Create(
            command.Recipient.Email,
            command.Recipient.PhoneNumber,
            command.Recipient.PushToken);

        if (recipientResult.IsFailure)
            return recipientResult.Error.ToErrors();

        var contentResult = Content.Create(
            command.Content.Subject,
            command.Content.Body);

        if (contentResult.IsFailure)
            return contentResult.Error.ToErrors();

        var notificationResult = Notification.Create(
            command.Channel,
            command.Type,
            recipientResult.Value,
            contentResult.Value);

        if (notificationResult.IsFailure)
            return notificationResult.Error.ToErrors();

        var notification = notificationResult.Value;

        await repository.AddAsync(notification, ct);

        await dispatcher.DispathAsync(notification, ct);
            
        await unitOfWork.SaveChangesAsync(ct);

        var queuedResult = notification.MarkQueued();
        if (queuedResult.IsFailure)
        {
            notification.MarkFailed();
            return queuedResult.Error.ToErrors();
        }

        await unitOfWork.SaveChangesAsync(ct);

        return notification.Id;
    }
}
