using Framework.Abstractions;
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Contracts.Requests;
using NotificationGateway.Domain;


namespace NotificationGateway.Application.Commands.CreateNotification;

public record SendNotificationCommand(
    NotificationChannel Channel,
    ContentDTO Content,
    RecipientDTO Recipient
) : ICommand
{
    public static SendNotificationCommand FromRequest(SendNotificationRequest request) => 
        new(
            request.Channel, 
            request.Content,
            request.Recipient);
}
