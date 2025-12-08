using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Database.Repository;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IRepository<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}