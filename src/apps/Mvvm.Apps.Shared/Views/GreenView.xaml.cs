namespace Mvvm.Apps.Views;

[ViewFor<GreenViewModel>(ViewModel = true)]
public partial class GreenView : UserControl
{
    public GreenView()
    {
        InitializeComponent();
    }
}