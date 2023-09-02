using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Views;

[ViewFor<MainViewModel>(ViewModel = true)]
public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
