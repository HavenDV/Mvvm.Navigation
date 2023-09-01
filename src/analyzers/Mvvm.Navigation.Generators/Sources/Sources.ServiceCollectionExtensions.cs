using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateServiceCollectionExtensionsDeclaration()
    {
        return @" 
#nullable enable

namespace Mvvm.Navigation
{
    public static partial class ServiceCollectionExtensions
    {
        static partial void AddViewsAndViewModelsInternal(
            global::Microsoft.Extensions.DependencyInjection.IServiceCollection services);

        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddViewsAndViewModels(
            this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            AddViewsAndViewModelsInternal(services);

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
        static partial void AddViewsAndViewModelsInternal(global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {{
            _ = services
{views.Select(property => @$"
                    .AddSingleton<{property.ViewModelType}>()
                    .AddTransient<IViewFor<{property.ViewModelType}>, {property.ViewType}>()
").Inject()}
                ;
        }}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}