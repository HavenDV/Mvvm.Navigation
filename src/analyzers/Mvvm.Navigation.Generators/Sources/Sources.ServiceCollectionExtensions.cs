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

        static partial void AddMappedViewsAndViewModels(
            global::Microsoft.Extensions.DependencyInjection.IServiceCollection services);

        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddMvvmNavigation(
            this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services = services ?? throw new global::System.ArgumentNullException(nameof(services));

            _ = services
                .AddTransient<global::Mvvm.Navigation.Navigator<global::CommunityToolkit.Mvvm.ComponentModel.ObservableObject>>()
                .AddSingleton<global::Mvvm.Navigation.IResolver, global::Mvvm.Navigation.Resolver>();

            AddMappedViewsAndViewModels(services);
            AddViewsAndViewModels(services);

            return services;
        }
    }
}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
    
    public static string GenerateServiceCollectionExtensionsImplementation(
        IReadOnlyCollection<ViewForData> views,
        string? prefix = null)
    {
        return @$" 
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Mvvm.Navigation
{{
    public static partial class ServiceCollectionExtensions
    {{
        static partial void Add{prefix}ViewsAndViewModels(global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {{
            services = services ?? throw new global::System.ArgumentNullException(nameof(services));

{views.Select(data => @$"
            _ = services
{(data.ViewModelLifetime is ServiceLifetime.None ? " " : @$" 
                .Add{data.ViewModelLifetime:G}<{data.ViewModelType}>()")}
{(data.ViewLifetime is ServiceLifetime.None ? " " : @$" 
{(data is { ViewModelConstructor: true } ? @$" 
                .Add{data.ViewLifetime:G}<{data.ViewType}>(static x => new {data.ViewType}(x.GetRequiredService<{data.ViewModelType}>()))" : @$" 
                .Add{data.ViewLifetime:G}<{data.ViewType}>()")}
                .Add{data.ViewLifetime:G}<IViewFor<{data.ViewModelType}>, {data.ViewType}>(static x => x.GetRequiredService<{data.ViewType}>())")}
                ;
").Inject()}
        }}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}