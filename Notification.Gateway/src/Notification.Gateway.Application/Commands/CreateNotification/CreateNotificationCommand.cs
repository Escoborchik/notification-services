using Framework.Abstractions;
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Domain;


namespace NotificationGateway.Application.Commands.CreateNotification;

public record CreateNotificationCommand(
    NotificationChannelType Channel,
    RecipientDTO Recipient
) : ICommand;
