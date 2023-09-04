using Mvvm.Navigation;
using Avalonia.Controls;

#nullable enable

// [assembly:MapViews(
//     viewsNamespace: nameof(H.Generators.IntegrationTests),
//     viewModelsNamespace: nameof(H.Generators.IntegrationTests))]

// ReSharper disable UnusedParameterInPartialMethod
// ReSharper disable RedundantExtendsListEntry
// ReSharper disable UnusedType.Global
// ReSharper disable IdentifierTypo

namespace H.Generators.IntegrationTests;

[ViewFor<MainViewModel>]
public partial class MainPage : UserControl;

public class MainViewModel;