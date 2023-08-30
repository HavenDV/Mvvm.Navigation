using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateIViewFor(ViewForData data)
    {
        return @$" 
#nullable enable

namespace {data.ViewNamespace}
{{
    public partial class {data.ViewClassName} : global::Mvvm.Navigation.IViewFor<{data.ViewModelType}>;
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}