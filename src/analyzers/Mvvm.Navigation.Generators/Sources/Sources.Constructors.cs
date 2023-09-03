using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateConstructors(ViewForData data)
    {
        return @$" 
#nullable enable

namespace {data.ViewNamespace}
{{
    public partial class {data.ViewClassName}
    {{
{(data is { InitializeComponent: true } ? @$" 
        public {data.ViewClassName}()
        {{
            InitializeComponent();
        }}" : " ")}
{(data is { ViewModelConstructor: true } ? @$"
        public {data.ViewClassName}({data.ViewModelType} viewModel) : this()
        {{
            ViewModel = viewModel;
{(data is { Activation: true } ? @$" 
            global::Mvvm.Navigation.Activation.Register(viewModel, this);
 " : " ")}
        }}" : " ")}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}