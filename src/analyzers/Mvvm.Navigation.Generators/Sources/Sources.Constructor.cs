using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateConstructor(ViewForData data)
    {
        return @$" 
#nullable enable

namespace {data.ViewNamespace}
{{
    public partial class {data.ViewClassName}
    {{
        public {data.ViewClassName}()
        {{
            InitializeComponent();
        }}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}