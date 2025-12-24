using HandlebarsDotNet;
using Microsoft.Extensions.Caching.Memory;
using Notifications.Contracts.Enums;

namespace EmailNotificationService.Renderer;

public class FileHandlebarsTemplateEngine(IMemoryCache cache) : ITemplateEngine
{
    private readonly string _basePath = Path.Combine(AppContext.BaseDirectory, Constants.TEMPLATES);

    public RenderedTemplate Render(NotificationType type, string subject, string body)
    {
        var key = $"{type}";

        var renderedSubject = RenderTemplate(key, Constants.SUBJECT_TEMPLATE, subject);
        var renderedBody = RenderTemplate(key, Constants.BODY_TEMPLATE, body);

        return new RenderedTemplate(renderedSubject, renderedBody);
    }

    private string RenderTemplate(string cacheKey, string file, object model)
    {
        var template = cache.GetOrCreate(cacheKey + file, _ =>
        {
            var path = Path.Combine(_basePath, cacheKey, file);

            return Handlebars.Compile(File.ReadAllText(path));
        });

        return template(model);
    }
}
