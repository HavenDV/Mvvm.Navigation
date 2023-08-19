using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Mvvm.Apps.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
