using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Apps.Views;

public partial class GreenView : UserControl
{
    public GreenViewModel ViewModel => (GreenViewModel)DataContext;
    
    #region Constructors

    public GreenView()
    {
        InitializeComponent();
        
        DataContext = Ioc.Default.GetRequiredService<GreenViewModel>();
    }

    #endregion
}
