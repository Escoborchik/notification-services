using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace EmailNotificationService.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("email_notification_service");

        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly,
            type => type.FullName?.Contains("Configurations") ?? false);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
