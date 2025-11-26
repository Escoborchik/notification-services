namespace NotificationGateway.Domain;

public class Recipient
{
    public string Email { get; }
    public string Phone { get; }
    public string DeviceToken { get; }

    public Recipient(string email, string phone, string deviceToken)
    {
        Email = email;
        Phone = phone;
        DeviceToken = deviceToken;
    }
}
