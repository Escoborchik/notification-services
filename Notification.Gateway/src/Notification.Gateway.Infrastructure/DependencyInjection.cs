using Framework.Database;
using Framework.Database.Repository;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Infrastructure.Migrator;
using Microsoft.EntityFrameworkCore;

namespace NotificationGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            options.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));

            options.UseLoggerFactory(
                LoggerFactory.Create(builder => builder.AddConsole()));
        });

        services.AddScoped<INotificationTransactionManager, NotificationTransactionManager>();

        services.AddRepositories(AssemblyReference.Assembly);

        services.AddScoped<IMigrator, NotificationMigrator>();

        services.AddMessageBus(configuration);


        return services;
    }

    private static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration["RabbitMQ:Host"]!), h =>
                {
                    h.Username(configuration["RabbitMQ:UserName"]!);
                    h.Password(configuration["RabbitMQ:Password"]!);
                });

                cfg.ConfigureEndpoints(context);
            });

            //configure.AddEntityFrameworkOutbox<AppDbContext>(o =>
            //{
            //    o.UsePostgres();
            //    o.UseBusOutbox();
            //});

            //configure.AddConfigureEndpointsCallback((context, name, cfg) =>
            //{
            //    cfg.UseEntityFrameworkOutbox<AppDbContext>(context);
            //});
        });

        return services;
    }
}
