using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

#nullable enable

// [assembly:MapViews(
//     viewsNamespace: nameof(H.Generators.IntegrationTests),
//     viewModelsNamespace: nameof(H.Generators.IntegrationTests))]

// ReSharper disable UnusedParameterInPartialMethod
// ReSharper disable RedundantExtendsListEntry
// ReSharper disable UnusedType.Global
// ReSharper disable IdentifierTypo

namespace Mvvm.Navigation.IntegrationTests;

[ViewFor<MainViewModel>(ViewModel = true)]
public partial class MainPage : UserControl
{
    public void InitializeComponent()
    {
    }
}

public class MainViewModel : ObservableObject;