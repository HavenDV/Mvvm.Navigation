using Uno.UI.Runtime.Skia.Wpf;

namespace Mvvm.Apps.Skia.Wpf;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var app = new System.Windows.Application();
        var host = new WpfHost(app.Dispatcher, () => new App());
        host.Run();
        app.Run();
    }
}