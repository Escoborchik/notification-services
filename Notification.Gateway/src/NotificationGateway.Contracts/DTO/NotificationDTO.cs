namespace NotificationGateway.Contracts.DTO;

public class NotificationDTO
{
    public string Channel { get; init; } = string.Empty;
    public RecipientDTO Recipient { get; init; } = null!;
    public ContentDTO Content { get; init; } = null!;
    public string Status { get; init; } = string.Empty;
    public string DeliveryStatus { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;

    public DateTime? SentAt { get; init; }
    public DateTime? FailedAt { get; init; }
    public string? ErrorMessage { get; init; }
}