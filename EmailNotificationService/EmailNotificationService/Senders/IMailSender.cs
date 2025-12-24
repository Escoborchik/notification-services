using EmailNotificationService.Models;

namespace EmailNotificationService.Senders;

public interface IMailSender
{
    Task SendAsync(OutgoingMessage message, CancellationToken ct);
}
