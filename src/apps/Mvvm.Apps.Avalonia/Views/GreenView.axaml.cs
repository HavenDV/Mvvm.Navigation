using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Views;

[ViewFor<GreenViewModel>(ViewModel = true, Constructor = true)]
public partial class GreenView : UserControl
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
