using EmailNotificationService.Services;
using MassTransit;
using Notifications.Contracts.Email;
using Notifications.Contracts.Status;

namespace EmailNotificationService.Consumers;

public class EmailNotificationEventConsumer(EmailSender sendEmailService, IPublishEndpoint _publish) : IConsumer<EmailNotificationEvent>
{
    public async Task Consume(ConsumeContext<EmailNotificationEvent> context)
    {
        var message = context.Message;

        try
        {
            await sendEmailService.Execute(message);

            await _publish.Publish(new NotificationSentEvent(
               message.NotificationId,
               Channel: "Email",
               SentAt: DateTime.UtcNow
           ));
        }
        catch (Exception ex)
        {
            

            await _publish.Publish(new NotificationFailedEvent(
                message.NotificationId,
                Channel: "Email",
                ErrorCode: "SMTP_ERROR",
                ErrorMessage: ex.Message,
                FailedAt: DateTime.UtcNow
            ));

            throw;
        }
    }
}