using CSharpFunctionalExtensions;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Router;

public interface INotificationRouter
{
    Task<UnitResult<Error>> RouteAsync(Notification notification, CancellationToken ct);
}
