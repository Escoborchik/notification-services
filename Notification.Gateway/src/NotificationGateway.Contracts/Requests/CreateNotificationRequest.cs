using NotificationGateway.Contracts.DTO;
using Notifications.Contracts.Enums;

namespace NotificationGateway.Contracts.Requests;

public record CreateNotificationRequest(
    NotificationChannel Channel,
    NotificationType Type,
    ContentDTO Content,
    RecipientDTO Recipient);