using CommunityToolkit.Mvvm.DependencyInjection;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Maui;

[ViewFor<MainViewModel>]
public partial class MainPage
{
	public MainPage()
	{
		InitializeComponent();
        
        BindingContext = Ioc.Default.GetRequiredService<MainViewModel>();
	}
}

