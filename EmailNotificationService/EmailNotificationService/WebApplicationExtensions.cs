using Framework.Database;
using Framework.Logging;
using Framework.Middlewares;

namespace EmailNotificationService;

public static class WebApplicationExtensions
{
    public async static Task Configure(this WebApplication app)
    {
        await app.Services.RunMigrations();

        app.UseExceptionMiddleware();
        app.UseApplicationRequestLogging();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.MapPrometheusScrapingEndpoint("/metrics");
    }
}
