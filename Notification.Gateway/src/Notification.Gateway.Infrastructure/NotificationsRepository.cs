using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Domain;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Infrastructure;

public class NotificationsRepository(AppDbContext dbContext) : INotificationsRepository
{
    public async Task<Guid> AddAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await dbContext.Notifications.AddAsync(notification, cancellationToken);

        return notification.Id;
    }

    public async Task<Result<Notification, Error>> GetByIdAsync(Guid notificationId, CancellationToken cancellationToken = default)
    {
        var notification = await dbContext.Notifications.SingleOrDefaultAsync(c => c.Id == notificationId, cancellationToken);

        if (notification is null)
            return GeneralErrors.City.NotFound(notificationId);

        return notification;
    }
}
