namespace NotificationGateway.Domain;

public class Notification
{
    public Guid Id { get; }
    public NotificationChannelType Channel { get; }
    public Recipient Recipient { get; }
    public Content Content { get; }
    public NotificationStatus Status { get; private set; }
    public int RetryCount { get; private set; }
    public string LastError { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    public Notification(
        Guid id,
        NotificationChannelType channel,
        Recipient recipient,
        Content content)
    {
        Id = id;
        Channel = channel;
        Recipient = recipient;
        Content = content;
        Status = NotificationStatus.Pending;
        RetryCount = 0;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void MarkProcessing()
    {
        Status = NotificationStatus.Processing;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkSent()
    {
        Status = NotificationStatus.Sent;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkDelivered()
    {
        Status = NotificationStatus.Delivered;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkFailed(string error)
    {
        Status = NotificationStatus.Failed;
        LastError = error;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementRetry(string error)
    {
        RetryCount++;
        LastError = error;
        UpdatedAt = DateTime.UtcNow;
    }
}
