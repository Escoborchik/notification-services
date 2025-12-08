using NotificationGateway.Contracts.DTO;

namespace NotificationGateway.Contracts.Messages;

public record NotificationCreatedEvent(
    Guid NotificationId,
    NotificationChannelDTO Channel,
    string RecipientEmail,
    string RecipientPhone,
    string RecipientPushToken,
    string Subject,
    string Message);
