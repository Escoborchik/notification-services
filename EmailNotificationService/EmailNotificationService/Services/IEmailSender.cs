using Notifications.Contracts.Email;

namespace EmailNotificationService.Services
{
    public interface IEmailSender
    {
        Task Execute(EmailNotificationEvent emailNotification);
    }
}