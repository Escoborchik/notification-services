namespace NotificationGateway.Domain;

public class Recipient
{
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? PushToken { get; private set; }

    public Recipient(string? email, string? phone, string? pushToken)
    {
        Email = email;
        PhoneNumber = phone;
        PushToken = pushToken;
    }


    // ef core
    private Recipient()
    {
        
    }
}
