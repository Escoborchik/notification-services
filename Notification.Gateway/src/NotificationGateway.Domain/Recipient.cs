namespace NotificationGateway.Domain;

public record Recipient
{
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? PushToken { get; private set; }

    public Recipient(string? email, string? phone, string? pushToken)
    {
        Email = email;
        Phone = phone;
        PushToken = pushToken;
    }
}
