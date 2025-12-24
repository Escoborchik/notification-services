using CSharpFunctionalExtensions;
using NotificationGateway.Domain.Enums;
using SharedKernel.EntityBase;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Domain;

public class Notification : AuditableEntity<Guid>
{
    public NotificationChannel Channel { get; private set; }
    public Recipient Recipient { get; private set; } = null!;
    public Content Content { get; private set; } = null!;
    public NotificationQueueStatus Status { get; private set; }
    public NotificationDeliveryStatus DeliveryStatus { get; private set; }
    public NotificationType Type { get; private set; }

    public DateTime? SentAt { get; private set; }
    public DateTime? FailedAt { get; private set; }
    public string? ErrorMessage { get; private set; }


    public static Result<Notification, Error> Create(
        NotificationChannel channel,
        NotificationType type,
        Recipient recipient,
        Content content)
    {
        var validationResult = Validate(channel, recipient, content);
        if (validationResult.IsFailure)
            return validationResult.Error;

        var notification = new Notification(Guid.NewGuid())
        {
            Channel = channel,
            Recipient = recipient,
            Content = content,
            Type = type,
            Status = NotificationQueueStatus.Pending
        };

        return notification;
    }

    public UnitResult<Error> MarkQueued()
    {
        if (Status != NotificationQueueStatus.Pending)
            return Error.Failure("notification.invalid-status-transition",
                "Invalid status transition from " + Status + " to " + NotificationQueueStatus.Queued);

        Status = NotificationQueueStatus.Queued;
        DeliveryStatus = NotificationDeliveryStatus.None;

        return UnitResult.Success<Error>();
    }

    public void MarkFailed()
    {

        Status = NotificationQueueStatus.Failed;
    }

    public void MarkSent(DateTime sentAt)
    {
        DeliveryStatus = NotificationDeliveryStatus.Sent;
        SentAt = sentAt;
    }

    public void MarkDeliveryFailed(string errorMessage, DateTime failedAt)
    {
        DeliveryStatus = NotificationDeliveryStatus.Failed;
        ErrorMessage = errorMessage;
        FailedAt = failedAt;
    }

    private static UnitResult<Error> Validate(
        NotificationChannel channel,
        Recipient recipient,
        Content content)
    {
        if (recipient is null)
            return Error.Failure("notification.invalid-recipient", "Recipient is required.");

        if (content is null)
            return Error.Failure("notification.invalid-content", "Content is required.");

        switch (channel)
        {
            case NotificationChannel.Email:
                if (string.IsNullOrWhiteSpace(recipient.Email))
                    return Error.Failure(
                        "notification.invalid-recipient",
                        "Email recipient is required for Email channel.");
                break;

            case NotificationChannel.Sms:
                if (string.IsNullOrWhiteSpace(recipient.Phone))
                    return Error.Failure(
                        "notification.invalid-recipient",
                        "Phone recipient is required for Sms channel.");
                break;

            case NotificationChannel.Push:
                if (string.IsNullOrWhiteSpace(recipient.PushToken))
                    return Error.Failure(
                        "notification.invalid-recipient",
                        "PushToken is required for Push channel.");
                break;

            default:
                return Error.Failure(
                    "notification.unsupported-channel",
                    $"Channel {channel} is not supported.");
        }

        return UnitResult.Success<Error>();
    }


    // ef core
    private Notification(Guid id) :base(id)
    {
    }
}