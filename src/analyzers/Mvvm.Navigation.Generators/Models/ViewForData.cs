namespace H.Generators;

public readonly record struct ViewForData(
    Framework Framework,
    string ViewType,
    string ViewClassName,
    string ViewNamespace,
    string ViewModelType,
    string ShortViewModelType,
    ServiceLifetime ViewLifetime,
    ServiceLifetime ViewModelLifetime);