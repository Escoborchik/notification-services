using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationGateway.Application.Interfaces;
using Notifications.Contracts.Events;

namespace NotificationGateway.Application.Features.Consumers;

public class NotificationSucceedFailedEventConsumer(
    INotificationRepository notificationRepository,
    INotificationTransactionManager uow,
    ILogger<NotificationSucceedSentEventConsumer> logger) 
    : IConsumer<NotificationFailedSentEvent>
{
    public async Task Consume(ConsumeContext<NotificationFailedSentEvent> context)
    {
        var message = context.Message;
        var ct = context.CancellationToken;

        var notification = await notificationRepository.FirstOrDefaultAsync(n => n.Id == message.NotificationId, ct);
        if (notification == null)
        {
            logger.LogWarning("Notification with ID {NotificationId} not found.", message.NotificationId);
            return;
        }
        notification.MarkDeliveryFailed(message.ErrorMessage, message.FailedAt);
        await uow.SaveChangesAsync(ct);
    }
}