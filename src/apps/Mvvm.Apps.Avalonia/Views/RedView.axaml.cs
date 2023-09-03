using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Views;

[ViewFor<RedViewModel>(ViewModel = true, InitializeComponent = true)]
public partial class RedView : UserControl
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
