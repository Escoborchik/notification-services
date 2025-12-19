namespace NotificationGateway.Domain;

public record Content
{
    public string Subject { get; }
    public string Text { get; }

    public Content(string subject, string text)
    {
        Subject = subject;
        Text = text;
    }
}
