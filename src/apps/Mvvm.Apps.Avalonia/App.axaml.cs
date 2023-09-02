using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvvm.Apps.ViewModels;
using Mvvm.Apps.Views;
using Mvvm.Navigation;

namespace Mvvm.Apps;

public class App : Application
{
    #region Properties

    private IHost? AppHost { get; set; }

    #endregion

    public override void Initialize()
    {
        AppHost = Host
            .CreateDefaultBuilder()
            .AddMvvmNavigation()
            .Build();

        Ioc.Default.ConfigureServices(AppHost.Services);
        
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = AppHost?.Services.GetRequiredService<MainView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
