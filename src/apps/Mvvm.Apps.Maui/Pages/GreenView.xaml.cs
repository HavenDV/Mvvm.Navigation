using CommunityToolkit.Mvvm.DependencyInjection;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Maui;

[ViewFor<GreenViewModel>]
public partial class GreenView
{
	public GreenView()
	{
		InitializeComponent();

        BindingContext = Ioc.Default.GetRequiredService<GreenViewModel>();
    }
}

