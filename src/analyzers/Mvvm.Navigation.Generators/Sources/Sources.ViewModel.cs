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
            get => ({data.ViewModelType}){dataContext};
            set => {dataContext} = value;
        }}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}