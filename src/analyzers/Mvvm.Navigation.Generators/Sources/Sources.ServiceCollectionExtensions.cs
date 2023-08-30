using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateServiceCollectionExtensions(IReadOnlyCollection<ViewForData> views)
    {
        return @$" 
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Mvvm.Navigation
{{
    public static class ServiceCollectionExtensions
    {{
        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddViewsAndViewModels(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection)
        {{
            return collection
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