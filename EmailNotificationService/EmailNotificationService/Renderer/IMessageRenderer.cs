using EmailNotificationService.Models;
using Notifications.Contracts.Enums;

namespace EmailNotificationService.Renderer;

public interface IMessageRenderer
{
    OutgoingMessage Render(NotificationType Code, string Subject, string Body);
}