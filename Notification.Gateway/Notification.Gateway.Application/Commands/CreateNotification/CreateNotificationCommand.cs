using NotificationGateway.Domain;


namespace NotificationGateway.Application.Commands.CreateNotification;

public record CreateNotificationCommand(
    NotificationChannelType Channel,
    Recipient Recipient,
    Content Content,
    string CorrelationId
);
