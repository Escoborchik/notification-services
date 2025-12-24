using Notifications.Contracts.Enums;

namespace EmailNotificationService.Renderer;

public interface ITemplateEngine
{
    RenderedTemplate Render(NotificationType type, string subject, string body);
}
