using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace H.Generators;

public static class PrepareData
{
    public static ViewForData GetViewForData(this AttributeData attribute, Framework framework, INamedTypeSymbol classSymbol)
    {
        attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));

        var viewType = classSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        var viewModelTypeSymbol =
            attribute.GetGenericTypeArgument(0) ??
            attribute.ConstructorArguments.ElementAtOrDefault(1).Value as ITypeSymbol;
        var viewModelType = viewModelTypeSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? string.Empty;
        var shortViewModelType = viewModelTypeSymbol?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? string.Empty;
        var constructor = attribute.GetNamedArgument("Constructor").ToBoolean();
        var viewModel = attribute.GetNamedArgument("ViewModel").ToBoolean();

        var fullClassName = classSymbol.ToString();
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
            Constructor: constructor,
            ViewModel: viewModel,
            ViewLifetime: ServiceLifetime.Transient,
            ViewModelLifetime: ServiceLifetime.Singleton);
    }
}