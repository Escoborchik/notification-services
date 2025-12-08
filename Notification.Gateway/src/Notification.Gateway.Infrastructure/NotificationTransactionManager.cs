using Framework.Database.Transaction;
using Microsoft.Extensions.Logging;
using NotificationGateway.Application.Interfaces;

namespace NotificationGateway.Infrastructure;

public class NotificationTransactionManager(
    AppDbContext dbContext,
    ILogger<NotificationTransactionManager> logger,
    ILoggerFactory loggerFactory) 
    : TransactionManagerBase<AppDbContext>(dbContext, logger, loggerFactory), INotificationTransactionManager;
