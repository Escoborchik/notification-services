namespace EmailNotificationService.Models;
public class MailData(IEnumerable<string> recievers, string subject, string body)
{

    public List<string> To { get; set; } = [.. recievers];

    public string Subject { get; } = subject;

    public string Body { get; } = body;
}