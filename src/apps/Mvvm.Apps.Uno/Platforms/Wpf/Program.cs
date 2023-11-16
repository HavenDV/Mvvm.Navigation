using Uno.UI.Runtime.Skia.Wpf;

namespace Mvvm.Apps.Skia.Wpf;

public class Program
{
	[STAThread]
	public static void Main(string[] args)
	{
		var app = new System.Windows.Application();
		var window = new System.Windows.Window
		{
			Content = new System.Windows.Controls.ContentControl
			{
				Content = new WpfHost(app.Dispatcher, () => new App()),
			}
		};
		
		window.Show();
		app.Run();
	}
}