using Framework.Logging;
using Framework.Swagger;
using NotificationGateway.Application;
using NotificationGateway.Infrastructure;
using System.Text.Json.Serialization;

namespace NotificationGateway.Presentation;

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
            .AddApplication()
            .AddInfrastructure()
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
            .AddCustomSwagger(configuration);

        return services;
    }
}
