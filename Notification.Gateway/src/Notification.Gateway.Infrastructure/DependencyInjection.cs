using Framework.Database;
using Microsoft.Extensions.DependencyInjection;
using NotificationGateway.Application.Interfaces;

namespace NotificationGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<INotificationsRepository, NotificationsRepository>();


        return services;
    }
}
