using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mvvm.Apps.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .AddMvvmNavigation()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();
        
        Ioc.Default.ConfigureServices(app.Services);

        return app;
    }
}
