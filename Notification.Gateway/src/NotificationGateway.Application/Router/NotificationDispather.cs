using CSharpFunctionalExtensions;
using MassTransit;
using NotificationGateway.Domain;
using NotificationGateway.Domain.Enums;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Router;

public class NotificationDispather(IEnumerable<INotificationRoute> routes) : INotificationDispatcher
{
    private readonly Dictionary<NotificationChannel, INotificationRoute> _routes = routes.ToDictionary(x => x.Channel, x => x);

    public async Task<UnitResult<Error>> DispathAsync(Notification notification, CancellationToken ct)
    {
        if (_routes.TryGetValue(notification.Channel, out var route))
        {
            await route.PublishAsync(notification, ct);
            return UnitResult.Success<Error>();
        }
        else
        {
            return  Error.Failure("notification.channel-not-supported",
                    $"Channel {notification.Channel} not supported.");
        }
    }
}