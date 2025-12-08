using SharedKernel.EntityBase;

namespace NotificationGateway.Domain;

public class Notification : AuditableEntity<Guid>
{
    public NotificationChannel Channel { get; private set; }
    public Recipient Recipient { get; private set; } = null!;
    public Content Content { get; private set; } = null!;
    public NotificationStatus Status { get; private set; }

    public static Notification Create(
        NotificationChannel channel,
        Recipient recipient,
        Content content)
    {
        var id = Guid.NewGuid();
        var notification = new Notification(id)
        {
            Channel = channel,
            Recipient = recipient,
            Content = content,
            Status = NotificationStatus.Pending,
        };

        return notification;
    }


    // ef core
    private Notification(Guid id) :base(id)
    {
    }
}