using NotificationGateway.Contracts.DTO;

namespace NotificationGateway.Contracts.Requests;

public record SendNotificationRequest(
    NotificationChannelDTO Channel,
    ContentDTO Content,
    RecipientDTO Recipient);