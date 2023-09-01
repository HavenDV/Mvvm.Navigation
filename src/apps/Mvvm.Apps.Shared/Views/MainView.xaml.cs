using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Apps.Views;

[ViewFor<MainViewModel>]
public partial class MainView
{
    public MainViewModel ViewModel
    {
        get => (MainViewModel)DataContext;
        set => DataContext = value;
    }

    #region Constructors

    public MainView()
    {
        InitializeComponent();
        
        ViewModel = Ioc.Default.GetRequiredService<MainViewModel>();
    }

    #endregion
}
