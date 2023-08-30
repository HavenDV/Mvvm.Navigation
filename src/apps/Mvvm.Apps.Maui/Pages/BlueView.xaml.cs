using CommunityToolkit.Mvvm.DependencyInjection;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Maui;

[ViewFor<BlueViewModel>]
public partial class BlueView
{
	public BlueView()
	{
		InitializeComponent();

        BindingContext = Ioc.Default.GetRequiredService<BlueViewModel>();
    }
}

