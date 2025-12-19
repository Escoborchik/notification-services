using NotificationGateway.Contracts.DTO;

namespace NotificationGateway.Contracts.Responses;

public class CreateNotificationResponse
{
    public Guid Id { get; set; }
    public NotificationStatusDTO Status { get; set; } = default!;
}