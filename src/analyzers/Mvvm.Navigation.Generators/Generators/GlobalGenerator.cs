using System.Collections.Immutable;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

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
                    AttributeData: x)))
            .Combine(framework)
            .SelectAndReportExceptions(PrepareData, context, Id)
            .WhereNotNull()
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .AddSource(context);
    }

    private static ViewForData? PrepareData(
        Framework framework,
        (SemanticModel SemanticModel, AttributeData AttributeData) tuple)
    {
        var (_, attribute) = tuple;

        return null;
    }

    private static EquatableArray<FileWithName> GetSourceCode(
        ViewForData data)
    {
        return new List<FileWithName>().ToImmutableArray();
    }

    #endregion
}