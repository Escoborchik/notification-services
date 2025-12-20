using CSharpFunctionalExtensions;
using EmailNotificationService.Models;
using Notifications.Contracts.Email;

namespace EmailNotificationService.Services;


public class EmailSender(
    SmtpEmailSender mailSender,
    HandlebarsTemplateService handlebarTemplateService) : IEmailSender
{
    public async Task Execute(EmailNotificationEvent emailNotification)
    {
        //var mailBody = _handlebarTemplateService.Process(new Dictionary<string, string>(), command.Template);

        var mailData = new MailData([emailNotification.Email], emailNotification.Subject, emailNotification.Body);

        await mailSender.Send(mailData);
    }
}