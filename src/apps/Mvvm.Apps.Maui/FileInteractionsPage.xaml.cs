using CommunityToolkit.Mvvm.DependencyInjection;
using Mvvm.Apps.ViewModels;

namespace Mvvm.Apps.Maui;

public partial class FileInteractionsPage
{
	public FileInteractionsPage()
	{
		InitializeComponent();

        BindingContext = Ioc.Default.GetRequiredService<FileInteractionsViewModel>();
    }
}

