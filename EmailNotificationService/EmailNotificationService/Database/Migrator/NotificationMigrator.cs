using Framework.Database;
using Microsoft.EntityFrameworkCore;

namespace EmailNotificationService.Database.Migrator;


public class NotificationMigrator(AppDbContext context, ILogger<NotificationMigrator> logger) : IMigrator
{
    public async Task Migrate(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Applying email service migrations...");

        await context.Database.MigrateAsync(cancellationToken);

        logger.Log(LogLevel.Information, "Migrations email service applied successfully.");
    }
}