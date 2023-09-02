using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Views;

[ViewFor<BlueViewModel>(ViewModel = true, Constructor = true)]
public partial class BlueView : UserControl
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
