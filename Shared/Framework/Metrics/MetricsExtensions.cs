using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Framework.Metrics;

public static class MetricsExtensions
{
    public static IServiceCollection AddApplicationMetrics(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceName =
            configuration["OpenTelemetry:ServiceName"] ?? "";

        services.AddOpenTelemetry()
            .ConfigureResource(resource =>
                resource.AddService(serviceName))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter();
            })
            .WithTracing(tracing => tracing
               .AddAspNetCoreInstrumentation()
               .AddHttpClientInstrumentation()
               .AddEntityFrameworkCoreInstrumentation()
               .AddSource(serviceName)
               .AddOtlpExporter());

        return services;
    }
}
