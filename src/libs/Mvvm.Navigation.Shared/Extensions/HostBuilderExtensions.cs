using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Mvvm.Navigation;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddMvvmNavigation(this IServiceCollection services)
    {
        services = services ?? throw new ArgumentNullException(nameof(services));

        _ = services
            .AddSingleton<Navigator<ObservableObject>>()
            .AddSingleton<IResolver, Resolver>()
            ;
        
        return services;
    }
}