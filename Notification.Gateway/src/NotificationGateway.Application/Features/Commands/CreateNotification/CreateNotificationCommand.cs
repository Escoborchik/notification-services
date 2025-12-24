using Framework.Abstractions;
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Contracts.Requests;
using NotificationGateway.Domain;
using NotificationGateway.Domain.Enums;


namespace NotificationGateway.Application.Features.Commands.CreateNotification;

public record CreateNotificationCommand(
    NotificationChannel Channel,
    NotificationType Type,
    ContentDTO Content,
    RecipientDTO Recipient
) : ICommand
{
    public static CreateNotificationCommand FromRequest(CreateNotificationRequest request) => 
        new(
            (NotificationChannel)request.Channel, 
            (NotificationType)request.Type,
            request.Content,
            request.Recipient);
}
