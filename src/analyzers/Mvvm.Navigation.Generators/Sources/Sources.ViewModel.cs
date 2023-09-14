using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateViewModel(ViewForData data)
    {
        var dataContext = data.Framework switch
        {
            Framework.Maui => "BindingContext",
            _ => "DataContext",
        };
        
        return @$" 
#nullable enable

namespace {data.ViewNamespace}
{{
    public partial class {data.ViewClassName}
    {{
        public {data.ViewModelType} ViewModel
        {{
            get => {dataContext} as {data.ViewModelType} ?? throw new global::System.ArgumentNullException(nameof({dataContext}));
            set => {dataContext} = value;
        }}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}