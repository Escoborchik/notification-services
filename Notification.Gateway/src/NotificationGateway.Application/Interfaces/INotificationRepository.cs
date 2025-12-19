using Framework.Database.Repository;
using NotificationGateway.Domain;

namespace NotificationGateway.Application.Interfaces;

public interface INotificationRepository : IRepository<Notification>;