using EmailNotificationService.Consumers;
using EmailNotificationService.Consumers.Definitions;
using EmailNotificationService.Database;
using EmailNotificationService.Options;
using EmailNotificationService.Services;
using Framework.Logging;
using MassTransit;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

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

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            options.UseNpgsql(configuration.GetConnectionString("Database"));

            options.UseLoggerFactory(
                LoggerFactory.Create(builder => builder.AddConsole()));
        });


        services
            .AddFramework(configuration);

        services.Configure<MailOptions>(configuration.GetSection(MailOptions.SECTION_NAME));
        services.AddScoped<SmtpEmailSender>();
        services.AddScoped<HandlebarsTemplateService>();
        services.AddScoped<EmailSender>();
        services.AddMemoryCache();
    }

    private static IServiceCollection AddFramework(
        this IServiceCollection services,
        IConfiguration configuration)
    
    {
        services
            .AddOpenApi()
            .AddEndpointsApiExplorer()
            .AddApplicationLoggingSeq(configuration)
            .AddMessageBus(configuration);

        return services;
    }

    private static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<EmailNotificationEventConsumer, SendEmailConsumerDefinition>();

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

            configure.AddEntityFrameworkOutbox<AppDbContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });

            configure.AddConfigureEndpointsCallback((context, name, cfg) =>
            {
                cfg.UseEntityFrameworkOutbox<AppDbContext>(context);
            });
        });

        return services;
    }
}
