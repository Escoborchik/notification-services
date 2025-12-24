using EmailNotificationService.Models;
using EmailNotificationService.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailNotificationService.Senders;

public sealed class SmtpMailSender(
    IOptions<SmtpOptions> options,
    ILogger<SmtpMailSender> logger) : IMailSender
{
    private readonly SmtpOptions _options = options.Value;

    public async Task SendAsync(OutgoingMessage message, CancellationToken ct)
    {

        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(_options.FromName, _options.FromEmail));
        mimeMessage.To.Add(MailboxAddress.Parse(message.Recipient));

        mimeMessage.Subject = message.Subject;
        mimeMessage.Body = new BodyBuilder
        {
            HtmlBody = message.Body
        }.ToMessageBody();

        using var client = new SmtpClient();

        var secureSocketOptions = _options.UseSsl
            ? SecureSocketOptions.StartTls
            : SecureSocketOptions.None;

        await client.ConnectAsync(_options.Host, _options.Port, secureSocketOptions, ct);

        if (!string.IsNullOrWhiteSpace(_options.UserName))
        {
            await client.AuthenticateAsync(_options.UserName, _options.Password, ct);
        }

        await client.SendAsync(mimeMessage, ct);
        await client.DisconnectAsync(true, ct);

        logger.LogInformation(
            "Email sent to {Recipient}", message.Recipient);
    }
}