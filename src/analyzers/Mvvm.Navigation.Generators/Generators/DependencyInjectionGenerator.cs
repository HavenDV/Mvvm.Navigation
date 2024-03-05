using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H.Generators;

[Generator]
public class DependencyInjectionGenerator : IIncrementalGenerator
{
    #region Constants

    private const string Id = "DIG";

    #endregion

    #region Methods

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var framework = context.TryDetectFramework();

        context.SyntaxProvider
            .ForAttributeWithMetadataName("Mvvm.Navigation.ViewForAttribute")
            .SelectManyAllAttributesOfCurrentClassSyntax()
            .Combine(framework)
            .SelectAndReportExceptions(PrepareData, context, Id)
            .WhereNotNull()
            .CollectAsEquatableArray()
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .AddSource(context);
        context.SyntaxProvider
            .ForAttributeWithMetadataName("Mvvm.Navigation.ViewForAttribute`1")
            .SelectManyAllAttributesOfCurrentClassSyntax()
            .Combine(framework)
            .SelectAndReportExceptions(PrepareData, context, Id)
            .WhereNotNull()
            .CollectAsEquatableArray()
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .AddSource(context);
        framework
            .SelectAndReportExceptions(GetFrameworkSpecificSourceCode, context, Id)
            .AddSource(context);
    }

    private static ViewForData? PrepareData(
        Framework framework,
        (SemanticModel SemanticModel, AttributeData AttributeData, ClassDeclarationSyntax ClassSyntax, INamedTypeSymbol
            ClassSymbol) tuple)
    {
        var (_, attribute, _, classSymbol) = tuple;

        return attribute.GetViewForData(framework, classSymbol);
    }

    private static EquatableArray<FileWithName> GetSourceCode(
        EquatableArray<ViewForData> values)
    {
        if (values.IsEmpty)
        {
            return ImmutableArray.Create<FileWithName>();
        }

        return ImmutableArray.Create(new FileWithName(
            Name: "ServiceCollectionExtensions.i.g.cs",
            Text: Sources.GenerateServiceCollectionExtensionsImplementation(values.AsImmutableArray())));
    }

    private static EquatableArray<FileWithName> GetFrameworkSpecificSourceCode(
        Framework framework)
    {
        var files = new List<FileWithName>();
        if (framework is not Framework.None)
        {
            files.Add(new FileWithName(
                Name: "ServiceCollectionExtensions.d.g.cs",
                Text: Sources.GenerateServiceCollectionExtensionsDeclaration()));
            files.Add(new FileWithName(
                Name: "HostBuilderExtensions.g.cs",
                Text: Sources.GenerateHostBuilderExtensions()));
        }
        if (framework is Framework.Maui)
        {
            files.Add(new FileWithName(
                Name: "MauiAppBuilderExtensions.g.cs",
                Text: Sources.GenerateMauiAppBuilderExtensions()));
        }

        return files.ToImmutableArray();
    }

    #endregion
}