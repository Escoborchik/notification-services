using Framework.Database.Repository;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Domain;

namespace NotificationGateway.Infrastructure;

public class NotificationsRepository(AppDbContext dbContext)
    : EFRepository<Notification, AppDbContext>(dbContext), INotificationRepository;
