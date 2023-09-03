namespace H.Generators;

public readonly record struct ViewForData(
    Framework Framework,
    string ViewType,
    string ViewFullName,
    string ViewClassName,
    string ViewNamespace,
    string ViewModelType,
    string ShortViewModelType,
    bool ViewModelConstructor,
    bool ViewModel,
    bool InitializeComponent,
    ServiceLifetime ViewLifetime,
    ServiceLifetime ViewModelLifetime);