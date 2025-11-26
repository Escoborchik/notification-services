
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Domain;

namespace NotificationGateway.Contracts;

public record CreateNotificationRequest(
    NotificationChannelType Channel,
    RecipientDTO Recipient);
