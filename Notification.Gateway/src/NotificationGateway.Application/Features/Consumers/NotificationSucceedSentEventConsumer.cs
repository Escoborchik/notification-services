using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Domain;
using Notifications.Contracts.Events;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Features.Consumers;

public class NotificationSucceedSentEventConsumer(
    INotificationRepository notificationRepository,
    INotificationTransactionManager uow,
    ILogger<NotificationSucceedSentEventConsumer> logger) 
    : IConsumer<NotificationSucceedSentEvent>
{
    public async Task Consume(ConsumeContext<NotificationSucceedSentEvent> context)
    {
        var message = context.Message;
        var ct = context.CancellationToken;

        var notification = await notificationRepository.FirstOrDefaultAsync(n => n.Id == message.NotificationId, ct);
        if (notification == null)
        {
            logger.LogWarning("Notification with ID {NotificationId} not found.", message.NotificationId);
            return;
        }
        notification.MarkSent(message.SentAt);
        await uow.SaveChangesAsync(ct);
    }
}

