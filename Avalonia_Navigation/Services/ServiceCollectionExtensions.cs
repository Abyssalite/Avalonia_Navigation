using Microsoft.Extensions.DependencyInjection;
using Avalonia_Navigation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNavigation(this IServiceCollection services)
    {
        services.AddSingleton<IViewHost, ViewHost>();
        services.AddSingleton<INavigatorService, NavigatorService>();
        return services;
    }
}