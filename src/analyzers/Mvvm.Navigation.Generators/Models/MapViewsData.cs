namespace H.Generators;

public readonly record struct MapViewsData(
    Framework Framework,
    string ViewsNamespace,
    string ViewModelsNamespace,
    bool ViewModelConstructor,
    bool ViewModel,
    bool InitializeComponent,
    bool Activation,
    ServiceLifetime ViewLifetime,
    ServiceLifetime ViewModelLifetime);