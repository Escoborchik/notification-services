using CSharpFunctionalExtensions;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Application.Interfaces
{
    public interface INotificationsRepository
    {
        Task<Guid> AddAsync(Notification notification, CancellationToken cancellationToken = default);
        Task<Result<Notification, Error>> GetByIdAsync(Guid notificationId, CancellationToken cancellationToken = default);
    }
}