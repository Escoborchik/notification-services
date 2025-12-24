using CSharpFunctionalExtensions;
using Framework.Abstractions;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Features.Queries;

public class GetNotificationQueryHandler(INotificationRepository notificationRepository) : IQueryHandler<Result<NotificationDTO, Errors>, GetNotificationQuery>
{
    public async Task<Result<NotificationDTO, Errors>> Execute(GetNotificationQuery query, CancellationToken ct)
    {
        var notification = await notificationRepository.FirstOrDefaultAsync(n => n.Id == query.NotificationId, ct);
        if (notification == null)
        {
            return GeneralErrors.NotFound(query.NotificationId, nameof(Notification)).ToErrors();
        }

        return new NotificationDTO()
        {
            Channel = notification.Channel.ToString(),
            Recipient = new RecipientDTO(notification.Recipient.Email, notification.Recipient.Phone, notification.Recipient.PushToken),
            Content = new ContentDTO(notification.Content.Subject, notification.Content.Body),
            Status = notification.Status.ToString(),
            DeliveryStatus = notification.DeliveryStatus.ToString(),
            Type = notification.Type.ToString(),
            SentAt = notification.SentAt,
            FailedAt = notification.FailedAt,
            ErrorMessage = notification.ErrorMessage
        };
    }
}