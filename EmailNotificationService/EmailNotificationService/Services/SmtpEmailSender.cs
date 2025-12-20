using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using EmailNotificationService.Options;
using EmailNotificationService.Models;

namespace EmailNotificationService.Services;

public class SmtpEmailSender(
    IOptions<MailOptions> options,
    ILogger<SmtpEmailSender> logger)
{
    private readonly MailOptions _options = options.Value;
    public async Task Send(MailData mailData)
    {
        var mail = new MimeMessage();

        mail.From.Add(new MailboxAddress(_options.FromDisplayName, _options.From));

        foreach (var address in mailData.To)
        {
            MailboxAddress.TryParse(address, out var mailAddress);
            mail.To.Add(mailAddress!);
        }

        var body = new BodyBuilder { HtmlBody = mailData.Body };

        mail.Body = body.ToMessageBody();
        mail.Subject = mailData.Subject;

        if (_options.Mode == "Pickup")
        {
            var path = Path.Combine(
                _options.PickupDirectory,
                $"{Guid.NewGuid()}.eml");

            await using var stream = File.Create(path);
            await mail.WriteToAsync(stream);
        }
        else
        {
            using var client = new SmtpClient();

            await client.ConnectAsync(_options.Host, _options.Port);
            await client.AuthenticateAsync(_options.UserName, _options.Password);
            await client.SendAsync(mail);
            await client.DisconnectAsync(true);
        }

        foreach (var address in mail.To)
            logger.LogInformation("Email succesfully sended to {to}", address);
    }
}