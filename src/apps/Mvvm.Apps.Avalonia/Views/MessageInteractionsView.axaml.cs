using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mvvm.Apps.ViewModels;

namespace Mvvm.Apps.Views;

public partial class MessageInteractionsView : UserControl
{
    public MessageInteractionsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
