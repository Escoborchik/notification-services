using EmailNotificationService.Database;
using Framework.Database;
using Microsoft.EntityFrameworkCore;

namespace EmailNotificationService.Migrator;


public class EmailServiceMigrator(AppDbContext context, ILogger<EmailServiceMigrator> logger) : IMigrator
{
    public async Task Migrate(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Applying email-service migrations...");

        await context.Database.MigrateAsync(cancellationToken);

        logger.Log(LogLevel.Information, "Migrations email-service applied successfully.");
    }
}