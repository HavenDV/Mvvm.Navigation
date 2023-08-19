using CommunityToolkit.Mvvm.DependencyInjection;
using Mvvm.Apps.ViewModels;

namespace Mvvm.Apps.Maui;

public partial class WebInteractionsPage
{
	public WebInteractionsPage()
	{
		InitializeComponent();

        BindingContext = Ioc.Default.GetRequiredService<WebInteractionsViewModel>();
    }
}

