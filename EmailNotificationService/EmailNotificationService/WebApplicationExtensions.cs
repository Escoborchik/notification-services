using Framework.Middlewares;
using Serilog;

namespace EmailNotificationService;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app)
    {
        app.UseExceptionMiddleware();
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
