using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H.Generators;

[Generator]
public class ConstructorGenerator : IIncrementalGenerator
{
    #region Constants

    private const string Id = "CG";

    #endregion

    #region Methods

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var framework = context.DetectFramework();
        
        context.SyntaxProvider
            .ForAttributeWithMetadataName("Mvvm.Navigation.ViewForAttribute")
            .SelectManyAllAttributesOfCurrentClassSyntax()
            .Combine(framework)
            .SelectAndReportExceptions(PrepareData, context, Id)
            .WhereNotNull()
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .AddSource(context);
        context.SyntaxProvider
            .ForAttributeWithMetadataName("Mvvm.Navigation.ViewForAttribute`1")
            .SelectManyAllAttributesOfCurrentClassSyntax()
            .Combine(framework)
            .SelectAndReportExceptions(PrepareData, context, Id)
            .WhereNotNull()
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .AddSource(context);
    }

    private static ViewForData? PrepareData(
        Framework framework,
        (SemanticModel SemanticModel, AttributeData AttributeData, ClassDeclarationSyntax ClassSyntax, INamedTypeSymbol
            ClassSymbol) tuple)
    {
        var (_, attribute, _, classSymbol) = tuple;

        var data = attribute.GetViewForData(framework, classSymbol);
        if (data is { ViewModelConstructor: false, InitializeComponent: false })
        {
            return null;
        }
        
        return data;
    }

    private static FileWithName GetSourceCode(
        ViewForData data)
    {
        return new FileWithName(
            Name: $"{data.ViewFullName}.Constructors.g.cs",
            Text: Sources.GenerateConstructors(data));
    }

    #endregion
}