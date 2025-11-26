namespace NotificationGateway.Domain;

public class Content
{
    public string Subject { get; }
    public string Text { get; }
    public string Html { get; }

    public Content(string subject, string text, string html)
    {
        Subject = subject;
        Text = text;
        Html = html;
    }
}
