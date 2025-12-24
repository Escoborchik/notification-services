using EmailNotificationService.Models;
using Notifications.Contracts.Abstractions;
using Notifications.Contracts.Enums;

namespace EmailNotificationService.Renderer;

public class MessageRenderer(ITemplateEngine templates) : IMessageRenderer
{
    public OutgoingMessage Render(NotificationType code, string subject, string body)
    {
        var rendered = templates.Render(code, subject, body);

        return new OutgoingMessage
        {
            Subject = rendered.Subject,
            Body = rendered.Body
        };
    }
}
