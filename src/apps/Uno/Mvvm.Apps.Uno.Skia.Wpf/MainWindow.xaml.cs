namespace Mvvm.Apps.Uno;

public partial class MainWindow : System.Windows.Window
{
    public MainWindow()
    {
        InitializeComponent();

        Root.Content = new global::Uno.UI.Skia.Platform.WpfHost(Dispatcher, () => new Mvvm.Apps.App());
    }
}
