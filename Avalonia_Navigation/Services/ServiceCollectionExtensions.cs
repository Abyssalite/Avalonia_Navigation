using Microsoft.Extensions.DependencyInjection;

namespace Avalonia_Navigation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAvaloniaNavigation(this IServiceCollection services)
    {
        services.AddSingleton<IViewHost, ViewHost>();
        services.AddSingleton<INavigatorService, NavigatorService>();
        return services;
    }
}