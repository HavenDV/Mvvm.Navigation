﻿using System.Collections.Immutable;
using H.Generators.Extensions;
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
        context.RegisterPostInitializationOutput(static context =>
        {
            context.AddSource(
                hintName: "ServiceCollectionExtensions.d.g.cs",
                source: Sources.GenerateServiceCollectionExtensionsDeclaration());
            context.AddSource(
                hintName: "HostBuilderExtensions.g.cs",
                source: Sources.GenerateHostBuilderExtensions());
        });
        
        var framework = context.DetectFramework();

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

        var files = new List<FileWithName>
        {
            new(Name: "ServiceCollectionExtensions.i.g.cs",
                Text: Sources.GenerateServiceCollectionExtensionsImplementation(values.AsImmutableArray())),
        };
        if (values.First().Framework is Framework.Maui)
        {
            files.Add(new FileWithName(
                Name: "MauiAppBuilderExtensions.g.cs",
                Text: Sources.GenerateMauiAppBuilderExtensions()));
        }

        return files.ToImmutableArray();
    }

    #endregion
}