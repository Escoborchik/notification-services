using NotificationGateway.Contracts.DTO;
using NotificationGateway.Domain;

namespace NotificationGateway.Contracts.Requests;

public record SendNotificationRequest(
    NotificationChannel Channel,
    ContentDTO Content,
    RecipientDTO Recipient);