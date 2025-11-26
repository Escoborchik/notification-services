using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Swagger;

public static class SwaggerExtensions
{
    /// <summary>
    /// Регистрирует Swagger документацию в коллекции сервисов, привязывая параметры API из конфигурации.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации Swagger.</param>
    /// <param name="configuration">Конфигурация приложения, содержащая секцию "Swagger".</param>
    /// <returns>Модифицированная коллекция сервисов с добавленным Swagger.</returns>
    public static IServiceCollection AddCustomSwagger(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Привязываем параметры описания API из конфигурационной секции "Swagger"
        var apiOptions = configuration.GetSection("Swagger").Get<ApiDescriptionOptions>()
                         ?? new ApiDescriptionOptions();

        services.AddSwaggerGen(options =>
        {
            ConfigureSwaggerDoc(options, apiOptions);

            var basePath = AppContext.BaseDirectory;
            var xmlFiles = Directory.GetFiles(basePath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var xmlFile in xmlFiles)
            {
                options.IncludeXmlComments(xmlFile, includeControllerXmlComments: true);
            }
        });

        return services;
    }

    /// <summary>
    /// Настраивает описание Swagger документа с использованием переданных параметров API.
    /// </summary>
    /// <param name="options">Объект настроек SwaggerGen.</param>
    /// <param name="apiOptions">Параметры описания API, полученные из конфигурации.</param>
    private static void ConfigureSwaggerDoc(SwaggerGenOptions options, ApiDescriptionOptions apiOptions)
    {
        //options.SwaggerDoc(
        //    apiOptions.Version,
        //    new OpenApiInfo
        //    {
        //        Title = apiOptions.Title,
        //        Version = apiOptions.Version,
        //        Description = apiOptions.Description,
        //        Contact = new OpenApiContact
        //        {
        //            Name = apiOptions.Contact.Name,
        //            Email = apiOptions.Contact.Email,
        //            Url = new Uri(apiOptions.Contact.Url),
        //        },
        //    });
    }
}
