namespace EmailNotificationService.Models;

public record OutgoingMessage
{
    public string Subject { get; init; } = string.Empty;
    public string Body { get; init; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
}
