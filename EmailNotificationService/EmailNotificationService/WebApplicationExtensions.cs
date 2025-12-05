using Framework.Middlewares;
using Serilog;

namespace EmailNotificationService;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
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
