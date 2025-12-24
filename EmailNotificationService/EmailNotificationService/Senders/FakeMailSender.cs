using EmailNotificationService.Models;
using System.Text;

namespace EmailNotificationService.Senders;

public class FakeMailSender(ILogger<FakeMailSender> logger) : IMailSender
{
    private readonly string _directoryPath = Path.Combine(AppContext.BaseDirectory, "mail-drop");

    public async Task SendAsync(OutgoingMessage message, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(message.Recipient))
            return;

        Directory.CreateDirectory(_directoryPath);

        var fileName = $"{DateTime.UtcNow:yyyyMMdd_HHmmss}_{Guid.NewGuid()}.eml";

        var filePath = Path.Combine(_directoryPath, fileName);

        var content = BuildEml(message.Recipient, message.Subject, message.Body);

        await File.WriteAllTextAsync(filePath, content, Encoding.UTF8,ct);

        logger.LogInformation("Email written to {FilePath}", filePath);
    }

    private static string BuildEml(
        string recipient,
        string subject,
        string body)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"To: {recipient}");
        sb.AppendLine("From: no-reply@local.dev");
        sb.AppendLine($"Subject: {subject}");
        sb.AppendLine("Content-Type: text/html; charset=utf-8");
        sb.AppendLine();
        sb.AppendLine(body);

        return sb.ToString();
    }
}