using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Apps.ViewModels;

namespace Mvvm.Apps.Views;

public partial class WebInteractionsView : UserControl
{
    public WebInteractionsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
