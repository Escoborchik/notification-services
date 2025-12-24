using EmailNotificationService.Models;
using EmailNotificationService.Renderer;
using EmailNotificationService.Senders;
using Notifications.Contracts.Events;

namespace EmailNotificationService.Services;


public class EmailService(
    IMessageRenderer renderer,
    IMailSender sender)
{
    public async Task Execute(EmailNotificationEvent emailNotification, CancellationToken ct)
    {
        var message = renderer.Render(emailNotification.NotificationType, emailNotification.Subject, emailNotification.Body);

        message.Recipient = emailNotification.Email;

        await sender.SendAsync(message, ct);
    }
}