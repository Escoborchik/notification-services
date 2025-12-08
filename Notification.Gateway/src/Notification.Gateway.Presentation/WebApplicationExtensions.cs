using Framework.Database;
using Framework.Middlewares;
using Serilog;

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

        app.UseExceptionMiddleware();
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        if (app.Environment.IsEnvironment("Docker"))
        {
            app.MapGet("/", () => Results.Redirect("/swagger"));
        }
    }
}
