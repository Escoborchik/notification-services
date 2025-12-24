using EmailNotificationService.Services;
using Framework.Services;
using MassTransit;
using Notifications.Contracts.Events;

namespace EmailNotificationService.Consumers;

public class EmailNotificationEventConsumer(
    EmailService sendEmailService,
    IPublishEndpoint _publish,
    ITimeProvider timeProvider) : IConsumer<EmailNotificationEvent>
{
    public async Task Consume(ConsumeContext<EmailNotificationEvent> context)
    {
        var message = context.Message;
        var ct = context.CancellationToken;

        try
        {
            await sendEmailService.Execute(message, ct);

            await _publish.Publish(new NotificationSucceedSentEvent(
               message.NotificationId,
               SentAt: timeProvider.UtcNow()
           ));
        }
        catch (Exception ex)
        {
            

            await _publish.Publish(new NotificationFailedSentEvent(
                message.NotificationId,
                ErrorMessage: ex.Message,
                FailedAt: timeProvider.UtcNow()
            ));

            throw;
        }
    }
}