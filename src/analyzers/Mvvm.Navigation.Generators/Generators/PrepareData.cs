using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H.Generators;

public static class Prepare
{
    public static ViewForData GetViewForData(
        Framework framework,
        INamedTypeSymbol viewSymbol,
        INamedTypeSymbol? viewModelSymbol,
        bool viewModelConstructor,
        bool initializeComponent,
        bool activation,
        bool viewModel,
        ServiceLifetime viewLifetime,
        ServiceLifetime viewModelLifetime)
    {
        var viewType = viewSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        var viewModelType = viewModelSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? string.Empty;
        var shortViewModelType = viewModelSymbol?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? string.Empty;

        var fullClassName = viewSymbol.ToString();
        var @namespace = fullClassName.Substring(0, fullClassName.LastIndexOf('.'));
        var className = fullClassName.Substring(fullClassName.LastIndexOf('.') + 1);
        
        return new ViewForData(
            Framework: framework,
            ViewType: viewType,
            ViewFullName: viewType.Replace("global::", string.Empty),
            ViewClassName: className,
            ViewNamespace: @namespace,
            ViewModelType: viewModelType,
            ShortViewModelType: shortViewModelType,
            ViewModelConstructor: viewModelConstructor,
            InitializeComponent: initializeComponent,
            Activation: activation,
            ViewModel: viewModel,
            ViewLifetime: viewLifetime,
            ViewModelLifetime: viewModelLifetime);
    }
    
    public static ViewForData GetViewForData(this AttributeData attribute, Framework framework, INamedTypeSymbol classSymbol)
    {
        attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));

        var viewModelTypeSymbol =
            (attribute.GetGenericTypeArgument(0) ??
            attribute.ConstructorArguments.ElementAtOrDefault(1).Value) as INamedTypeSymbol;
        var initializeComponent = attribute.GetNamedArgument(nameof(ViewForData.InitializeComponent)).ToBoolean();
        var viewModel = attribute.GetNamedArgument(nameof(ViewForData.ViewModel)).ToBoolean();
        var viewModelConstructor = attribute.GetNamedArgument(nameof(ViewForData.ViewModelConstructor)).ToBoolean(defaultValue: viewModel);
        var activation = attribute.GetNamedArgument(nameof(ViewForData.Activation)).ToBoolean(defaultValue: viewModel);

        var viewLifetime = attribute.GetNamedArgument(nameof(ViewForData.ViewLifetime))
            .ToEnum<ServiceLifetime>() ?? ServiceLifetime.Transient;
        var viewModelLifetime = attribute.GetNamedArgument(nameof(ViewForData.ViewModelLifetime))
            .ToEnum<ServiceLifetime>() ?? ServiceLifetime.Singleton;

        return GetViewForData(
            framework: framework,
            viewSymbol: classSymbol,
            viewModelSymbol: viewModelTypeSymbol,
            viewModelConstructor: viewModelConstructor,
            initializeComponent: initializeComponent,
            activation: activation,
            viewModel: viewModel,
            viewLifetime: viewLifetime,
            viewModelLifetime: viewModelLifetime);
    }
    
    public static MapViewsData GetMapViewsData(this AttributeData attribute, Framework framework, CompilationUnitSyntax compilationUnitSyntax)
    {
        attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));

        var arguments = compilationUnitSyntax.AttributeLists.FirstOrDefault()?.Attributes.FirstOrDefault()?.ArgumentList?.Arguments;
        var viewsNamespace = arguments.HasValue
            ? arguments.Value[0].Expression.ToString().Replace("nameof(", string.Empty).Trim(')', ' ')
            : string.Empty;
        var viewModelsNamespace = arguments.HasValue
            ? arguments.Value[1].Expression.ToString().Replace("nameof(", string.Empty).Trim(')', ' ')
            : string.Empty;
        
        var initializeComponent = attribute.GetNamedArgument(nameof(MapViewsData.InitializeComponent)).ToBoolean();
        var viewModel = attribute.GetNamedArgument(nameof(MapViewsData.ViewModel)).ToBoolean();
        var viewModelConstructor = attribute.GetNamedArgument(nameof(MapViewsData.ViewModelConstructor)).ToBoolean(defaultValue: viewModel);
        var activation = attribute.GetNamedArgument(nameof(MapViewsData.Activation)).ToBoolean(defaultValue: viewModel);

        var viewLifetime = attribute.GetNamedArgument(nameof(MapViewsData.ViewLifetime))
            .ToEnum<ServiceLifetime>() ?? ServiceLifetime.Transient;
        var viewModelLifetime = attribute.GetNamedArgument(nameof(MapViewsData.ViewModelLifetime))
            .ToEnum<ServiceLifetime>() ?? ServiceLifetime.Singleton;

        return new MapViewsData(
            Framework: framework,
            ViewsNamespace: viewsNamespace,
            ViewModelsNamespace: viewModelsNamespace,
            ViewModelConstructor: viewModelConstructor,
            InitializeComponent: initializeComponent,
            Activation: activation,
            ViewModel: viewModel,
            ViewLifetime: viewLifetime,
            ViewModelLifetime: viewModelLifetime);
    }
}