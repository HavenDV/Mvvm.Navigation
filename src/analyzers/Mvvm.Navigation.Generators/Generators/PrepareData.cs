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

        var fullClassName = classSymbol.ToString();
        var @namespace = fullClassName.Substring(0, fullClassName.LastIndexOf('.'));
        var className = fullClassName.Substring(fullClassName.LastIndexOf('.') + 1);
        
        return new ViewForData(
            Framework: framework,
            ViewType: viewType,
            ViewClassName: className,
            ViewNamespace: @namespace,
            ViewModelType: viewModelType,
            ShortViewModelType: shortViewModelType,
            ViewLifetime: ServiceLifetime.Transient,
            ViewModelLifetime: ServiceLifetime.Singleton);
    }
}