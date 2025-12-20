using Framework.Abstractions;
using Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using NotificationGateway.Application.Router;
using TimeProvider = Framework.Services.Implementation.TimeProvider;

namespace NotificationGateway.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        //services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        services.AddHandlers(AssemblyReference.Assembly);

        services.AddSingleton<ITimeProvider, TimeProvider>();

        services.AddScoped<INotificationRouter, NotificationRouter>();
        services.AddScoped<INotificationRoute, EmailNotificationRoute>();

        return services;
    }
}
