using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mvvm.Apps.ViewModels;
using Mvvm.Navigation;

namespace Mvvm.Apps.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMvvmCommonInteractions()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services
            .AddSingleton<MainViewModel>()
            .AddSingleton<FileInteractionsViewModel>()
            .AddSingleton<MessageInteractionsViewModel>()
            .AddSingleton<WebInteractionsViewModel>()
            ;
        
		var app = builder.Build();
        
        Ioc.Default.ConfigureServices(app.Services);

        return app;
    }
}
