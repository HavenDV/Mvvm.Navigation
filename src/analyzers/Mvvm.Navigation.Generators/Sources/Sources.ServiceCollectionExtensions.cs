using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateServiceCollectionExtensionsDeclaration()
    {
        return @" 
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
                    .AddSingleton<global::Mvvm.Navigation.Navigator<global::CommunityToolkit.Mvvm.ComponentModel.ObservableObject>>()
                    .AddSingleton<global::Mvvm.Navigation.IResolver, global::Mvvm.Navigation.Resolver>()
                ;

            AddViewsAndViewModels(services);

            return services;
        }
    }
}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
    
    public static string GenerateServiceCollectionExtensionsImplementation(IReadOnlyCollection<ViewForData> views)
    {
        return @$" 
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Mvvm.Navigation
{{
    public static partial class ServiceCollectionExtensions
    {{
        static partial void AddViewsAndViewModels(global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {{
            _ = services
{views.Select(property => @$"
                    .AddSingleton<{property.ViewModelType}>()
                    .AddTransient<{property.ViewType}>()
                    .AddTransient<IViewFor<{property.ViewModelType}>, {property.ViewType}>(static x => x.GetRequiredService<{property.ViewType}>())
").Inject()}
                ;
        }}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}