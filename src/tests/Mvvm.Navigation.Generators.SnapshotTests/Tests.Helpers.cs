using Microsoft.CodeAnalysis;
using H.Generators.Tests.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace H.Generators.SnapshotTests;

[TestClass]
public partial class Tests : VerifyBase
{
    private static string GetHeader(
        Framework framework,
        bool nullable,
        bool mapViews,
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

{(mapViews ? @"[assembly:MapViews(
    viewsNamespace: nameof(H.Generators.IntegrationTests),
    viewModelsNamespace: nameof(H.Generators.IntegrationTests),
    ViewModel = true,
    InitializeComponent = true)]" : string.Empty)}

namespace H.Generators.IntegrationTests;

public static class MyServiceCollectionExtensions
{{
    public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection My(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
    {{
        return services.AddMvvmNavigation();
    }}
    
    public static global::Microsoft.Extensions.Hosting.IHostBuilder My(this global::Microsoft.Extensions.Hosting.IHostBuilder builder)
    {{
        return builder.AddMvvmNavigation();
    }}
    
    public static global::Microsoft.Extensions.Hosting.HostApplicationBuilder My(this global::Microsoft.Extensions.Hosting.HostApplicationBuilder builder)
    {{
        return builder.AddMvvmNavigation();
    }}
{(framework == Framework.Maui ? @"
    
    public static global::Microsoft.Maui.Hosting.MauiAppBuilder My(this global::Microsoft.Maui.Hosting.MauiAppBuilder builder)
    {
        return builder.AddMvvmNavigation();
    }" : string.Empty)}
}}

public partial class MainPage
{{
    private void InitializeComponent()
    {{
    }}
}}
";
    }

    private static string GetHeader(
        Framework framework,
        params string[] values)
    {
        return GetHeader(framework, nullable: true, mapViews: false, values);
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

    private async Task CheckSourceAsync(
        string source,
        Framework framework,
        bool verifyFiles = true,
        [CallerMemberName]string? callerName = null,
        CancellationToken cancellationToken = default)
    {
        if (framework == Framework.Maui)
        {
            source = source
                .Replace("using Microsoft.Maui.Controls;", string.Empty)
                .Replace("UserControl", "Grid");
            source = @$"using Microsoft.Maui.Controls;
{source}";
        }
        
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
        }).AddPackages(ImmutableArray.Create(
            new PackageIdentity("Microsoft.Extensions.Hosting", "7.0.1"),
            new PackageIdentity("CommunityToolkit.Mvvm", "8.2.1")));
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
        
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
                generators: new IIncrementalGenerator[]
                    {
                        new InterfaceGenerator(),
                        new ConstructorGenerator(),
                        new DependencyInjectionGenerator(),
                        new ViewModelGenerator(),
                        new GlobalGenerator(),
                    }
                    .Select(GeneratorExtensions.AsSourceGenerator)
                    .ToArray(),
                parseOptions: CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview));
        driver = driver
            .WithUpdatedAnalyzerConfigOptions(new DictionaryAnalyzerConfigOptionsProvider(GetGlobalOptions(framework)))
            .RunGeneratorsAndUpdateCompilation(compilation, out compilation, out _, cancellationToken);
        var diagnostics = compilation.GetDiagnostics(cancellationToken);

        var tasks = new List<Task>
        {
            Verify(diagnostics
                    .Where(static x =>
                        x.Severity == DiagnosticSeverity.Error &&
                        x.Id != "CS0234")
                    .ToImmutableArray()
                    .NormalizeLocations())
                .UseDirectory($"Snapshots/{callerName}/{framework:G}")
                //.AutoVerify()
                .UseTextForParameters($"{framework}_Diagnostics"),
        };
        if (verifyFiles)
        {
            tasks.Add(
                Verify(driver)
                    .UseDirectory($"Snapshots/{callerName}/{framework:G}")
                    //.AutoVerify()
                    .UseTextForParameters($"{framework}"));
        }
        
        await Task.WhenAll(tasks);
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