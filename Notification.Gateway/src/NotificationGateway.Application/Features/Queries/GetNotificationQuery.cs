using Framework.Abstractions;

namespace NotificationGateway.Application.Features.Queries;

public record GetNotificationQuery(Guid NotificationId) : IQuery;
