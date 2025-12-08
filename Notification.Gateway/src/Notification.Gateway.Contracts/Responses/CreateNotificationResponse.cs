using NotificationGateway.Domain;

namespace NotificationGateway.Contracts.Responses;

public class CreateNotificationResponse
{
    public Guid Id { get; set; }
    public NotificationStatus Status { get; set; } = default!;
}