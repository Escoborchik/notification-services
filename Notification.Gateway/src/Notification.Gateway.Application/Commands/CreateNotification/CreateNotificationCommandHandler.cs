using CSharpFunctionalExtensions;
using Framework.Abstractions;
using Framework.Database;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Commands.CreateNotification;

public class CreateNotificationCommandHandler(
    INotificationsRepository notificationsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<Guid, CreateNotificationCommand>
{
    public async Task<Result<Guid, Errors>> Execute(CreateNotificationCommand command, CancellationToken ct)
    {
        var recipient = new Recipient(
            command.Recipient.Email,
            string.Empty,
            string.Empty
        );
        var notification = new Notification(
            Guid.NewGuid(),
            command.Channel,
            recipient
        );

        await notificationsRepository.AddAsync(notification, ct);
        await unitOfWork.SaveChanges(ct);


        return notification.Id;
    }
}
