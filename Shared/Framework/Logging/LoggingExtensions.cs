using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Framework.Logging;

public static class LoggingExtensions
{
    public static IHostBuilder UseApplicationLogging(this IHostBuilder host, IConfiguration configuration)
    {
        return host.UseSerilog((context, services, logger) =>
        {
            var env = context.HostingEnvironment;

            logger
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)

                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Service", env.ApplicationName)
                .Enrich.WithProperty("Environment", env.EnvironmentName)

                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3} {Service}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.Seq(
                     configuration.GetConnectionString("Seq")
                    ?? throw new InvalidOperationException("Seq connection string missing")
                );
        });
    }

    public static IApplicationBuilder UseApplicationRequestLogging(
        this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate =
                "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

            options.EnrichDiagnosticContext = (ctx, http) =>
            {
                ctx.Set(
                    "ClientIP",
                    http.Connection.RemoteIpAddress?.ToString() ?? "unknown");

                ctx.Set(
                    "UserAgent",
                    http.Request.Headers.UserAgent.ToString() ?? "unknown");

                ctx.Set(
                    "RequestHost",
                    string.IsNullOrWhiteSpace(http.Request.Host.Value)
                        ? "unknown"
                        : http.Request.Host.Value);
            };
        });
    }
}