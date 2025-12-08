using Framework.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NotificationGateway.Infrastructure.Migrator;


public class NotificationMigrator(AppDbContext context, ILogger<NotificationMigrator> logger) : IMigrator
{
    public async Task Migrate(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Applying booking migrations...");

        await context.Database.MigrateAsync(cancellationToken);

        logger.Log(LogLevel.Information, "Migrations booking applied successfully.");
    }
}