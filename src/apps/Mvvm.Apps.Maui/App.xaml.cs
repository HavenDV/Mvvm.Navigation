using Mvvm.Apps.Views;

namespace Mvvm.Apps;

public partial class App
{
	public App(MainView mainView)
	{
		InitializeComponent();

        MainPage = mainView;
	}
}
