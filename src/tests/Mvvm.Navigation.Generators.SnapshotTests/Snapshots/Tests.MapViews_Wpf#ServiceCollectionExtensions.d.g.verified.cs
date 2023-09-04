//HintName: ServiceCollectionExtensions.d.g.cs
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Mvvm.Navigation
{
    public static partial class ServiceCollectionExtensions
    {
        static partial void AddViewsAndViewModels(
            global::Microsoft.Extensions.DependencyInjection.IServiceCollection services);

        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddMvvmNavigation(
            this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services = services ?? throw new global::System.ArgumentNullException(nameof(services));

            _ = services
                    .AddTransient<global::Mvvm.Navigation.Navigator<global::CommunityToolkit.Mvvm.ComponentModel.ObservableObject>>()
                    .AddSingleton<global::Mvvm.Navigation.IResolver, global::Mvvm.Navigation.Resolver>()
                ;

            AddViewsAndViewModels(services);

            return services;
        }
    }
}