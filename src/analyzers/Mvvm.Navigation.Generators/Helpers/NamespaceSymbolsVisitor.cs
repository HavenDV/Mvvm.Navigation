using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace H.Generators;

public class NamespaceSymbolsVisitor : SymbolVisitor
{
    public string Namespace { get; init; } = string.Empty;
    public IReadOnlyCollection<INamedTypeSymbol> OutputSymbols { get; private set; } = Array.Empty<INamedTypeSymbol>();
    
    public override void VisitNamespace(INamespaceSymbol symbol)
    {
        Parallel.ForEach(symbol.GetNamespaceMembers(),
            x => x.Accept(this));
        
        var @namespace = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        if (@namespace == Namespace.WithGlobalPrefix())
        {
            OutputSymbols = symbol.GetTypeMembers();
        }
    }
}