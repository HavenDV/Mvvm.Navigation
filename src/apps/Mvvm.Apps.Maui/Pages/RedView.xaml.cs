using CommunityToolkit.Mvvm.DependencyInjection;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Maui;

[ViewFor<RedViewModel>]
public partial class RedView
{
	public RedView()
	{
		InitializeComponent();

        BindingContext = Ioc.Default.GetRequiredService<RedViewModel>();
    }
}

