using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Apps.Views;

[ViewFor<RedViewModel>]
public partial class RedView : UserControl
{
    public RedViewModel ViewModel => (RedViewModel)DataContext;
    
    #region Constructors

    public RedView()
    {
        InitializeComponent();
        
        DataContext = Ioc.Default.GetRequiredService<RedViewModel>();
    }

    #endregion
}
