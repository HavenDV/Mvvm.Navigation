using Microsoft.CodeAnalysis;

namespace H.Generators;

public static class Matcher
{
    public static IReadOnlyCollection<(INamedTypeSymbol View, INamedTypeSymbol ViewModel)> Match(
        IReadOnlyCollection<INamedTypeSymbol> views,
        IReadOnlyCollection<INamedTypeSymbol> viewModels)
    {
        var pairs = new List<(INamedTypeSymbol View, INamedTypeSymbol ViewModel)>();

        var viewModelsByName = viewModels
            .Select(static x => (Name: x.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat), ViewModel: x))
            .Where(static x => x.Name.Contains("ViewModel"))
            .ToDictionary(static x => x.Name, static x => x.ViewModel);
        foreach (var view in views)
        {
            var viewName = view.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            if (viewName.Contains("ViewModel"))
            {
                continue;
            }
            
            foreach (var possibleName in new[]
            {
                viewName.Replace("Page", "ViewModel"),
                viewName.Replace("View", "ViewModel"),
                viewName.Replace("Control", "ViewModel"),
                viewName.Replace("Window", "ViewModel"),
            })
            {
                if (viewModelsByName.TryGetValue(possibleName, out var viewModel))
                {
                    pairs.Add((view, viewModel));
                }
            }
        }
        
        return pairs;
    }
}