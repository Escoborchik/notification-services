using Framework.Abstractions;
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Contracts.Requests;
using NotificationGateway.Domain;


namespace NotificationGateway.Application.Commands.CreateNotification;

public record CreateNotificationCommand(
    NotificationChannel Channel,
    ContentDTO Content,
    RecipientDTO Recipient
) : ICommand
{
    public static CreateNotificationCommand FromRequest(CreateNotificationRequest request) => 
        new(
            (NotificationChannel)request.Channel, 
            request.Content,
            request.Recipient);
}
