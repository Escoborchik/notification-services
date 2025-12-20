using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace NotificationGateway.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly,
            type => type.FullName?.Contains("Configurations") ?? false);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
