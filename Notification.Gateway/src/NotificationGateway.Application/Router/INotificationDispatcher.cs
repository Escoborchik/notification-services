using CSharpFunctionalExtensions;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Router;

public interface INotificationDispatcher
{
    Task<UnitResult<Error>> DispathAsync(Notification notification, CancellationToken ct);
}
