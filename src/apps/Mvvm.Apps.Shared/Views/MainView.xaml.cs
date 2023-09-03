namespace Mvvm.Apps.Views;

[ViewFor<MainViewModel>(ViewModel = true)]
public partial class MainView : Page
{
    public MainView()
    {
        InitializeComponent();
    }
}
