namespace Mvvm.Apps.Views;

[ViewFor<BlueViewModel>(ViewModel = true)]
public partial class BlueView : UserControl
{
    public BlueView()
    {
        InitializeComponent();
    }
}