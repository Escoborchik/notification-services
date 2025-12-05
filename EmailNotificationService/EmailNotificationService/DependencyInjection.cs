using Framework.Logging;
using Framework.Swagger;
using MassTransit;
using System.Text.Json.Serialization;

namespace EmailNotificationService;

public static class DependencyInjection
{
    public static void AddProgramDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            

        services
            .AddFramework(configuration);
    }

    private static IServiceCollection AddFramework(
        this IServiceCollection services,
        IConfiguration configuration)
    
    {
        services
            .AddOpenApi()
            .AddEndpointsApiExplorer()
            .AddApplicationLoggingSeq(configuration)
            .AddCustomSwagger(configuration)
            .AddMessageBus(configuration);

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
        });

        return services;
    }
}
