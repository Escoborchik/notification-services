using NotificationGateway.Contracts.DTO;

namespace NotificationGateway.Contracts.Requests;

public record CreateNotificationRequest(
    NotificationChannelDTO Channel,
    ContentDTO Content,
    RecipientDTO Recipient);