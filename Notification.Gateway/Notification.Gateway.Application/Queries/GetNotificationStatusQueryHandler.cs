

namespace NotificationGateway.Application.Queries;

public class GetNotificationStatusQueryHandler
{
    private readonly INotificationRepository _repository;

    public GetNotificationStatusQueryHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationStatusResult> Handle(GetNotificationStatusQuery query)
    {
        var notification = await _repository.GetByIdAsync(query.NotificationId);
        if (notification == null)
            throw new KeyNotFoundException("Notification not found");

        return new NotificationStatusResult(
            notification.Id,
            notification.Status,
            notification.RetryCount,
            notification.LastError
        );
    }
}
