using System.Collections.Immutable;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H.Generators;

[Generator]
public class GlobalGenerator : IIncrementalGenerator
{
    #region Constants

    private const string Id = "GG";

    #endregion

    #region Methods

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var framework = context.DetectFramework();
        
        context.SyntaxProvider
            .ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: "Mvvm.Navigation.MapViewsAttribute",
                predicate: static (_, _) => true,
                transform: static (context, _) => context)
            .SelectMany(static (context, _) => context.Attributes
                .Select(x => (
                    context.SemanticModel,
                    context.TargetNode,
                    context.TargetSymbol,
                    AttributeData: x)))
            .Combine(framework)
            .SelectAndReportExceptions(PrepareData, context, Id)
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .AddSource(context);
    }

    private static EquatableArray<ViewForData> PrepareData(
        Framework framework,
        (SemanticModel SemanticModel, SyntaxNode TargetNode, ISymbol TargetSymbol, AttributeData AttributeData) tuple)
    {
        var (semanticModel, targetNode, _, attribute) = tuple;
        var data = attribute.GetMapViewsData(framework, (CompilationUnitSyntax)targetNode);
        var viewsVisitor = new NamespaceSymbolsVisitor
        {
            Namespace = data.ViewsNamespace,
        };
        viewsVisitor.Visit(semanticModel.Compilation.GlobalNamespace);
        var viewModelsVisitor = new NamespaceSymbolsVisitor
        {
            Namespace = data.ViewModelsNamespace,
        };
        viewModelsVisitor.Visit(semanticModel.Compilation.GlobalNamespace);

        var pairs = Matcher.Match(viewsVisitor.OutputSymbols, viewModelsVisitor.OutputSymbols);
        
        var initializeComponent = attribute.GetNamedArgument(nameof(MapViewsData.InitializeComponent)).ToBoolean();
        var viewModel = attribute.GetNamedArgument(nameof(MapViewsData.ViewModel)).ToBoolean();
        var viewModelConstructor = attribute.GetNamedArgument(nameof(MapViewsData.ViewModelConstructor)).ToBoolean(defaultValue: viewModel);
        var activation = attribute.GetNamedArgument(nameof(MapViewsData.Activation)).ToBoolean(defaultValue: viewModel);
        
        return pairs
            .Select(x => Prepare.GetViewForData(
                framework: framework,
                viewSymbol: x.View,
                viewModelSymbol: x.ViewModel,
                viewModelConstructor: viewModelConstructor,
                initializeComponent: initializeComponent,
                activation: activation,
                viewModel: viewModel))
            .ToImmutableArray();
    }

    private static EquatableArray<FileWithName> GetSourceCode(
        EquatableArray<ViewForData> values)
    {
        if (values.IsEmpty)
        {
            return ImmutableArray.Create<FileWithName>();
        }

        var files = new List<FileWithName>();
        foreach (var data in values)
        {
            if (data is not { ViewModelConstructor: false, InitializeComponent: false })
            {
                files.Add(new FileWithName(
                    Name: $"{data.ViewFullName}.Constructors.g.cs",
                    Text: Sources.GenerateConstructors(data)));
            }
            if (data is { ViewModel: true })
            {
                files.Add(new FileWithName(
                    Name: $"{data.ViewFullName}.ViewModel.g.cs",
                    Text: Sources.GenerateViewModel(data)));
            }
            
            files.Add(new FileWithName(
                Name: $"{data.ViewFullName}.IViewFor.{data.ShortViewModelType}.g.cs",
                Text: Sources.GenerateIViewFor(data)));
        }
        
        files.Add(new FileWithName(
            Name: "ServiceCollectionExtensions.i.g.cs",
            Text: Sources.GenerateServiceCollectionExtensionsImplementation(values.AsImmutableArray(), prefix: "Global")));
        
        return files.ToImmutableArray();
    }

    #endregion
}