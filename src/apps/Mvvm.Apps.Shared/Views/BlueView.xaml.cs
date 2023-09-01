using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Apps.Views;

[ViewFor<BlueViewModel>]
public partial class BlueView : UserControl
{
    public BlueViewModel ViewModel => (BlueViewModel)DataContext;
    
    #region Constructors

    public BlueView()
    {
        InitializeComponent();
        
        DataContext = Ioc.Default.GetRequiredService<BlueViewModel>();
    }

    #endregion
}