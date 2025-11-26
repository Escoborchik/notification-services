using NotificationGateway.Domain;
namespace NotificationGateway.Application.Commands.UpdateNotification;

public class UpdateNotificationStatusCommandHandler(
    INotificationRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task Handle(UpdateNotificationStatusCommand command)
    {
        var notification = await repository.GetByIdAsync(command.NotificationId);
        if (notification == null)
            throw new KeyNotFoundException("Notification not found");

        switch (command.Status)
        {
            case NotificationStatus.Processing:
                notification.MarkProcessing();
                break;
            case NotificationStatus.Sent:
                notification.MarkSent();
                break;
            case NotificationStatus.Delivered:
                notification.MarkDelivered();
                break;
            case NotificationStatus.Failed:
                notification.MarkFailed(command.ErrorMessage);
                break;
        }

        await repository.UpdateAsync(notification);
        await unitOfWork.SaveChangesAsync();
    }
}
