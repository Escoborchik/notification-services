
using NotificationGateway.Domain;

namespace NotificationGateway.Application.Commands.UpdateNotification;

public record UpdateNotificationStatusCommand(
 Guid NotificationId,
 NotificationStatus Status,
 string ErrorMessage
);
