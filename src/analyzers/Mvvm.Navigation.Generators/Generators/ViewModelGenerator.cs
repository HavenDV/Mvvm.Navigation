﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H.Generators;

[Generator]
public class ViewModelGenerator : IIncrementalGenerator
{
    #region Constants

    private const string Id = "VMG";

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
        if (!data.ViewModel)
        {
            return null;
        }
        
        return data;
    }

    private static FileWithName GetSourceCode(
        ViewForData data)
    {
        return new FileWithName(
            Name: $"{data.ViewFullName}.ViewModel.g.cs",
            Text: Sources.GenerateViewModel(data));
    }

    #endregion
}