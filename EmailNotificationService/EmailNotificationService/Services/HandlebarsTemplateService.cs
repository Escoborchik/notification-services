using HandlebarsDotNet;
using Microsoft.Extensions.Caching.Memory;

namespace EmailNotificationService.Services;

public class HandlebarsTemplateService(IMemoryCache cache)
{
    public string Process(Dictionary<string, string> details, string templateKey)
    {
        if (!cache.TryGetValue(templateKey, out HandlebarsTemplate<object, object>? compiledTemplate))
        {
            compiledTemplate = Handlebars.Compile(
                File.ReadAllText(Path.Combine(
                    Directory.GetCurrentDirectory(), 
                    "wwwroot", 
                    "Templates", 
                    $"{templateKey}.html")));

            cache.Set(templateKey, compiledTemplate);
        }

        if (compiledTemplate is null)
            throw new ApplicationException("Template error");

        return compiledTemplate(details);
    }
}