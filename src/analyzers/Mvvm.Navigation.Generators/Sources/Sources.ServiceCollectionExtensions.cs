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
                    .AddTransient<global::Mvvm.Navigation.Navigator<global::CommunityToolkit.Mvvm.ComponentModel.ObservableObject>>()
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
            services = services ?? throw new global::System.ArgumentNullException(nameof(services));

{views.Select(data => @$"
            _ = services
                    .AddSingleton<{data.ViewModelType}>()
{(data is { ViewModelConstructor: true } ? @$" 
                    .AddTransient<{data.ViewType}>(static x => new {data.ViewType}(x.GetRequiredService<{data.ViewModelType}>()))" : @$" 
                    .AddTransient<{data.ViewType}>()")}
                    .AddTransient<IViewFor<{data.ViewModelType}>, {data.ViewType}>(static x => x.GetRequiredService<{data.ViewType}>());
").Inject()}
        }}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}