using Framework.Database;
using Framework.Logging;

namespace NotificationGateway.Presentation;

public static class WebApplicationExtensions
{
    public async static Task Configure(this WebApplication app)
    {

        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
            await app.Services.RunMigrations();
        }

        app.UseApplicationRequestLogging();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.MapPrometheusScrapingEndpoint("/metrics");

        if (app.Environment.IsEnvironment("Docker"))
        {
            app.MapGet("/", () => Results.Redirect("/swagger"));
        }
    }
}
