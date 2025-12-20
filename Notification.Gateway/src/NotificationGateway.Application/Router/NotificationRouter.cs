using CSharpFunctionalExtensions;
using MassTransit;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Router;

public class NotificationRouter : INotificationRouter
{
    private readonly Dictionary<NotificationChannel, INotificationRoute> _routes;

    public NotificationRouter(IEnumerable<INotificationRoute> routes)
    {
        _routes = routes.ToDictionary(x => x.Channel, x => x);
    }

    public async Task<UnitResult<Error>> RouteAsync(Notification notification, CancellationToken ct)
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