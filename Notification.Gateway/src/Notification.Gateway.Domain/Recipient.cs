namespace NotificationGateway.Domain;

public class Recipient
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    //public string Phone { get; private set }
    //public string DeviceToken { get; private set }

    public Recipient(string email, string phone, string deviceToken)
    {
        Email = email;
        //Phone = phone;
        //DeviceToken = deviceToken;
    }


    // ef core
    private Recipient()
    {
        
    }
}
