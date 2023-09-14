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
        partial void BeforeInitializeComponent();
        partial void AfterInitializeComponent();

{(data is { InitializeComponent: true } ? @$" 
        public {data.ViewClassName}()
        {{
            BeforeInitializeComponent();
            InitializeComponent();
            AfterInitializeComponent();
        }}" : " ")}
{(data is { ViewModelConstructor: true } ? @$"
        public {data.ViewClassName}({data.ViewModelType} viewModel)
        {{
            ViewModel = viewModel;

            BeforeInitializeComponent();
            InitializeComponent();
            AfterInitializeComponent();

{(data is { Activation: true } ? @" 
            global::Mvvm.Navigation.Activation.Register(viewModel, this);
 " : " ")}
        }}" : " ")}
    }}
}}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}