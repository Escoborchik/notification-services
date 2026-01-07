using EmailNotificationService.Consumers;
using EmailNotificationService.Consumers.Definitions;
using EmailNotificationService.Database;
using EmailNotificationService.Migrator;
using EmailNotificationService.Options;
using EmailNotificationService.Renderer;
using EmailNotificationService.Senders;
using EmailNotificationService.Services;
using Framework.Database;
using Framework.Metrics;
using Framework.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TimeProvider = Framework.Services.Implementation.TimeProvider;

namespace EmailNotificationService;

public static class DependencyInjection
{
    public static void AddProgramDependencies(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            options.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));

            options.UseLoggerFactory(
                LoggerFactory.Create(builder => builder.AddConsole()));
        });

        services.AddScoped<IMigrator, EmailServiceMigrator>();


        services
            .AddFramework(configuration)
            .AddEmailServices(configuration, environment);
    }

    private static IServiceCollection AddFramework(this IServiceCollection services, IConfiguration configuration)
    
    {
        services
            .AddOpenApi()
            .AddEndpointsApiExplorer()
            .AddApplicationMetrics(configuration)
            .AddMessageBus(configuration);

        services.AddSingleton<ITimeProvider,TimeProvider>();

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

    private static IServiceCollection AddEmailServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)

    {
        services.AddScoped<EmailService>();
        services.AddMemoryCache();

        services.AddScoped<IMessageRenderer, MessageRenderer>();
        services.AddScoped<ITemplateEngine, FileHandlebarsTemplateEngine>();

        if (environment.IsDevelopment() || environment.IsEnvironment("Docker"))
        {
            services.AddScoped<IMailSender, FakeMailSender>();
        }
        else
        {
            services.AddScoped<IMailSender, SmtpMailSender>();
        }

        services.Configure<SmtpOptions>(
            configuration.GetSection(SmtpOptions.SMTP));

        return services;
    }
}
