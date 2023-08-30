using Microsoft.CodeAnalysis;
using H.Generators.Tests.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Immutable;

namespace H.Generators.SnapshotTests;

[TestClass]
public partial class Tests : VerifyBase
{
    private static string GetHeader(
        Framework framework,
        bool nullable,
        params string[] values)
    {
        var prefix = framework switch
        {
            Framework.WinUi or Framework.UnoWinUi => @"Microsoft.UI.Xaml",
            Framework.Uwp or Framework.Uno => @"Windows.UI.Xaml",
            Framework.Avalonia => @"Avalonia",
            Framework.Maui => @"Microsoft.Maui",
            _ => @"System.Windows",
        };
        var usings = string.Join(
            Environment.NewLine,
            values.Select(value => string.IsNullOrWhiteSpace(value)
                ? $"using {prefix};"
                : value.StartsWith("System")
                    ? $"using {value};"
                    : $"using {prefix}.{value};"));

        return @$"{usings}
using Mvvm.Navigation;

#nullable {(nullable ? "enable" : "disable")}

namespace H.Generators.IntegrationTests;
";
    }

    private static string GetHeader(
        Framework framework,
        params string[] values)
    {
        return GetHeader(framework, true, values);
    }

    private static Dictionary<string, string> GetGlobalOptions(Framework framework)
    {
        var globalOptions = new Dictionary<string, string>();
        if (framework == Framework.Wpf)
        {
            globalOptions.Add("build_property.UseWPF", "true");
        }
        else if (framework == Framework.WinUi)
        {
            globalOptions.Add("build_property.UseWinUI", "true");
        }
        else if (framework == Framework.Maui)
        {
            globalOptions.Add("build_property.UseMaui", "true");
        }
        else if (framework == Framework.Uwp)
        {
            globalOptions.Add($"build_property.RecognizeFramework_DefineConstants", "WINDOWS_UWP");
        }
        else if (framework == Framework.Uno)
        {
            globalOptions.Add($"build_property.RecognizeFramework_DefineConstants", "HAS_UNO");
        }
        else if (framework == Framework.UnoWinUi)
        {
            globalOptions.Add($"build_property.RecognizeFramework_DefineConstants", "HAS_UNO;HAS_WINUI");
        }
        else if (framework == Framework.Avalonia)
        {
            globalOptions.Add($"build_property.RecognizeFramework_DefineConstants", "HAS_AVALONIA");
        }

        return globalOptions;
    }

    private async Task CheckSourceAsync<T>(
        string source,
        Framework framework,
        CancellationToken cancellationToken = default,
        params IIncrementalGenerator[] additionalGenerators)
        where T : IIncrementalGenerator, new()
    {
        var referenceAssemblies = (framework switch
        {
            Framework.None => ReferenceAssemblies.NetFramework.Net48.Wpf,
            Framework.Wpf => ReferenceAssemblies.NetFramework.Net48.Wpf,
            Framework.Uwp => FrameworkReferenceAssemblies.Net70Uwp,
            Framework.WinUi => FrameworkReferenceAssemblies.Net70WinUi,
            Framework.Uno => FrameworkReferenceAssemblies.Net70Uno,
            Framework.UnoWinUi => FrameworkReferenceAssemblies.Net70UnoWinUi,
            Framework.Avalonia => FrameworkReferenceAssemblies.Net70Avalonia,
            Framework.Maui => FrameworkReferenceAssemblies.Net70Maui,
            _ => throw new NotImplementedException(),
        }).AddPackages(ImmutableArray.Create(new PackageIdentity("Microsoft.Extensions.DependencyInjection.Abstractions", "7.0.0")));
        var references = await referenceAssemblies.ResolveAsync(null, cancellationToken);
        var compilation = (Compilation)CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(source, options: new CSharpParseOptions(LanguageVersion.Preview),
                    cancellationToken: cancellationToken),
            },
            references: references
                .Add(MetadataReference.CreateFromFile(
                    typeof(Mvvm.Navigation.ViewForAttribute<>).Assembly.Location)),
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var generator = new T();
        GeneratorDriver driver = additionalGenerators.Any()
            ? CSharpGeneratorDriver.Create(new IIncrementalGenerator[] { generator }.Concat(additionalGenerators)
                .ToArray())
            : CSharpGeneratorDriver.Create(generator);
        driver = driver
            .WithUpdatedAnalyzerConfigOptions(new DictionaryAnalyzerConfigOptionsProvider(GetGlobalOptions(framework)))
            .RunGeneratorsAndUpdateCompilation(LanguageVersion.Preview, compilation, out compilation, out _,
                cancellationToken);
        var diagnostics = compilation.GetDiagnostics(cancellationToken);

        await Task.WhenAll(
            Verify(diagnostics.NormalizeLocations())
                .UseDirectory("Snapshots")
                .UseTextForParameters($"{framework}_Diagnostics"),
            Verify(driver)
                .UseDirectory("Snapshots")
                .UseTextForParameters($"{framework}"));
    }
}

internal static class StringExtensions
{
    internal static string ReplaceType(this string source, string from, string to)
    {
        return source
            .Replace($": {from}", $": global::{to}")
            .Replace($"{from}.", $"global::{to}.")
            .Replace($", {from}", $", global::{to}");
    }
}